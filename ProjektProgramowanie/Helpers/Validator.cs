namespace ProjektProgramowanie
{
    public static class CarValidator
    {
        private static bool LenghtOfData(string data, int requiredLenght) => !string.IsNullOrEmpty(data) || data.Length == requiredLenght;

        internal static string CheckLongOfInsertedData(Car carToValidate)
        {
            bool IsVinValid = LenghtOfData(carToValidate.Vin, 17);
            bool IsRegisterNumberValid = LenghtOfData(carToValidate.RegistrationNumber, 7);
            if (!IsVinValid && !IsRegisterNumberValid)
            {
                carToValidate.RegistrationNumber = "";
                carToValidate.Vin = "";
                return "Vin number requires 17 characters and Registration number requires 7 characters!";
            }
            if (!IsVinValid)
            {
                carToValidate.RegistrationNumber = "";
                return "Vin number requires 17 characters!";
            }
            if (!IsRegisterNumberValid)
            {
                carToValidate.Vin = "";
                return "Registration number requires 7 characters!";
            }
            return "";
        }
    }
}
