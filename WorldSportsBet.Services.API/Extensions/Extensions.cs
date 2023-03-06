namespace WorldSportsBet.Services.API.Extensions
{
    public static class Extensions
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static bool IsDefault<T>(this T value) where T : struct
        {
            bool isDefault = value.Equals(default(T));

            return isDefault;
        }
        #endregion

        #region Constructors
        #endregion

    }
}
