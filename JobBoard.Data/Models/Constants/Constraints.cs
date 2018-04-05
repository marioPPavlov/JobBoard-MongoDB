using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Data.Models.Constants
{
    public static class Constraints
    {
        public const int NameMax = 50;
        public const int NameMin = 3;

        public const int DescriptionMax = 10000;
        public const int DescriptionMin = 10;

        public const int TagMax = 200;
        public const int TagMin = 2;

        public const int PictureMax = 1000;
        public const int PictureMin = 7;
    }
}
