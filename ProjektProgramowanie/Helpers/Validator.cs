using ProjektProgramowanie.Model;

namespace ProjektProgramowanie
{
    public static class CarValidator
    {
        private static bool LenghtOfData(string data, int requiredLenght) => string.IsNullOrEmpty(data) || data.Length == requiredLenght;

        internal static string CheckLongOfInsertedData(Car carToValidate, Registration registration)
        {
            bool IsVinValid = LenghtOfData(carToValidate.Vin, 17);
            bool IsRegisterNumberValid = CheckIsRegisterNumberValid(registration);
            if (!IsVinValid && !IsRegisterNumberValid)
            {
                registration.RegistrationNumber = "";
                carToValidate.Vin = "";
                return "Vin number requires 17 characters and Registration data is wrong";
            }
            if (!IsVinValid)
            {
                registration.RegistrationNumber = "";
                return "Vin number requires 17 characters!";
            }
            if (!IsRegisterNumberValid)
            {
                carToValidate.Vin = "";
                return "Registration data is wrong";
            }
            return "";
        }
        private static bool CheckIsRegisterNumberValid(Registration registration)
        {
            return registration == null || !string.IsNullOrEmpty(registration.OwnerName) || registration.RegistrationDate != null
                || !LenghtOfData(registration.RegistrationNumber, 7);
        }
    }
}
