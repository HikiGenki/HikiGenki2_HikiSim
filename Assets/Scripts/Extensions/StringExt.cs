public static class StringExt {
    public static bool EqualsIgnoreCase(this string s, string s2) {
        return s.ToLower().Equals(s2.ToLower());
    }
}