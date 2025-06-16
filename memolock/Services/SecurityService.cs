using System.Security.Cryptography;
using System.Text;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;

namespace memolock.Services
{
    public class SecurityService
    {
        // 앱 마스터 비밀번호 키
        private const string AppPasswordKey = "app_master_password";

        // 보안 설정 완료 여부 키
        private const string SetupCompleteKey = "is_security_setup_complete";

        // 잠금 방식 키
        private const string LockMethodKey = "lock_method";

        // 비밀번호를 해시하여 저장하고 반환합니다.
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        // 입력된 비밀번호가 저장된 해시와 일치하는지 확인합니다.
        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        // 앱 마스터 비밀번호를 SecureStorage에 저장합니다.
        public async Task SetAppPasswordAsync(string password)
        {
            var hash = HashPassword(password);
            await SecureStorage.SetAsync(AppPasswordKey, hash);
        }

        // 저장된 앱 마스터 비밀번호와 입력 비밀번호를 비교합니다.
        public async Task<bool> VerifyAppPasswordAsync(string password)
        {
            var storedHash = await SecureStorage.GetAsync(AppPasswordKey);
            if (string.IsNullOrEmpty(storedHash)) return false;
            return VerifyPassword(password, storedHash);
        }

        // 보안 설정 완료 상태를 저장합니다.
        public void SetSetupComplete() => Preferences.Set(SetupCompleteKey, true);

        // 보안 설정 완료 여부를 확인합니다.
        public bool IsSetupComplete() => Preferences.Get(SetupCompleteKey, false);

        // 잠금 방식을 저장합니다. (Password, Biometric, Pattern)
        public void SetLockMethod(string method) => Preferences.Set(LockMethodKey, method);

        // 저장된 잠금 방식을 가져옵니다.
        public string GetLockMethod() => Preferences.Get(LockMethodKey, "Password");

        // 생체 인증을 시도합니다.
        public async Task<bool> AuthenticateByBiometricsAsync()
        {
            var request = new AuthenticationRequestConfiguration("memolock 인증", "앱 잠금을 해제하려면 인증하세요.");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            return result.Authenticated;
        }
    }
}
