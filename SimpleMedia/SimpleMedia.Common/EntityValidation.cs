namespace SimpleMedia.Common
{
    public static class EntityValidation
    {
        public static class Post
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 100;
            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 1000;

            public const string CreatedOnColumnType = "datetime2";
        }

        public static class Comment
        {
            public const int ContentMinLength = 1;
            public const int ContentMaxLength = 500;

            public const string CreatedOnColumnType = "datetime2";
        }

        public static class User
        {
            public const int UserNameMinLength = 3;
            public const int UserNameMaxLength = 15;
        }

        public static class Tag
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }
    }
}
