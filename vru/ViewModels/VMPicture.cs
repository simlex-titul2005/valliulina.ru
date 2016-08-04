﻿using System;

namespace vru.ViewModels
{
    public sealed class VMPicture
    {
        public Guid Id { get; set; }

        public string ImgFormat { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public int Size { get; set; }
    }
}