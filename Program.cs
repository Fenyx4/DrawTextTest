using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using System.IO;
using System.Diagnostics;

namespace DrawTextTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var Regions = new List<Region>();
            Console.WriteLine("Hello World!");

            Regions.Add(new Region
            {
                Name = "The Deep Forest",
                RunicName = "Äe DÁp Forest"
            });

            Regions.Add(new Region
            {
                Name = "Isle of Deeds",
                RunicName = "Isle of DÁds"
            });

            Regions.Add(new Region
            {
                Name = "The High Stepes",
                RunicName = "Äe High Stepes"
            });

            Regions.Add(new Region
            {
                Name = "Fens of the Dead",
                RunicName = "Fens of the DÀd"
            });

            foreach(var region in Regions)
            {
                Console.WriteLine(region.Name);
                Console.WriteLine(region.RunicName);
            }

            var image = new SixLabors.ImageSharp.Image<Rgba32>(256, 256);

            TextGraphicsOptions options = new TextGraphicsOptions(new SixLabors.ImageSharp.GraphicsOptions()
            {
            },
            new TextOptions
            {
                ApplyKerning = true,
                TabWidth = 8, // a tab renders as 8 spaces wide
                                //WrapTextWidth = 100, // greater than zero so we will word wrap at 100 pixels wide
                HorizontalAlignment = HorizontalAlignment.Center // right align
            });

            FontCollection collection = new FontCollection();
            using (var fontStream = new MemoryStream(Resource1.runes))
            {
                Console.WriteLine("Creating image!");
                FontFamily family = collection.Install(fontStream);
                Font font = family.CreateFont(22, FontStyle.Regular);
                for (int i = 0; i < Regions.Count; i++)
                {
                    var region = Regions[i];
                    image.Mutate(x => x.DrawText(options, region.RunicName.ToUpper(), font, SixLabors.ImageSharp.Color.Black, new SixLabors.ImageSharp.PointF(128, i * 20)));
                }
                Console.WriteLine("Saving Image!");
                image.SaveAsPng("image.png");

                new Process
                {
                    StartInfo = new ProcessStartInfo("image.png")
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }
    }
    public class Region
    {
        public string Name { get; internal set; }
        public string RunicName { get; internal set; }
    }
}
