using CalendarioVale.Data.Model;

namespace CalendarioVale;

public static class BiometricsDtoExtension
{

    public static Biometrics ToBiometrics(this BiometricsDto dto)
    {
        return new Biometrics(dto.Value, dto.Type, dto.DateReading);
    }
}
