﻿using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"C:\Users\Jero\Desktop\ProgII\ReposPipe\PII_Pipes_Filters_Start\src\Program\luke.jpg");

            // Crea los objetos Filter en orden inverso
            IFilter filter2 = new FilterNegative();
            IFilter filter1 = new FilterGreyscale();

            // Crea los objetos Pipe en orden inverso
            IPipe pipe3 = new PipeNull();
            IPipe pipe2 = new PipeSerial(filter2, pipe3);
            IPipe pipe1 = new PipeSerial(filter1, pipe2);

            // Envia la imagen a través de los Pipes y guarda las imágenes intermedias
            picture = pipe1.Send(picture);

            // Guarda la imagen intermedia después de FilterGreyscale
            provider.SavePicture(picture, @"C:\Users\Jero\Desktop\ProgII\ReposPipe\PII_Pipes_Filters_Start\src\Program\intermediate1.jpg");

            // Continua con la secuencia
            picture = pipe2.Send(picture);

            // Guarda la imagen intermedia después de FilterNegative
            provider.SavePicture(picture, @"C:\Users\Jero\Desktop\ProgII\ReposPipe\PII_Pipes_Filters_Start\src\Program\intermediate2.jpg");

            // Continua con la secuencia
            picture = pipe3.Send(picture);

            // Guarda la imagen final
            provider.SavePicture(picture, @"C:\Users\Jero\Desktop\ProgII\ReposPipe\PII_Pipes_Filters_Start\src\Program\final.jpg");
        }
    }
}

