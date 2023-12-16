namespace EPortalAdmin.Core.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object value) =>
           value is not null;

        public static bool IsNull(this object value) =>
            value is null;

        public static bool Is(this object value, Func<bool> func) =>
            func();

        public static bool IsNot(this object value, Func<bool> func) =>
            !func();

        public static bool IsFalse(this bool value) =>
            !value;

        public static bool IsTrue(this bool value) =>
            value;

        public static bool IsEmptyCollection<T>(this ICollection<T> collection)
            where T : class =>
            collection.Any().IsFalse();

        public static bool IsNotEmptyCollection<T>(this ICollection<T> collection)
            where T : class =>
            collection.Any().IsTrue();
    }
}
