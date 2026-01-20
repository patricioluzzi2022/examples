using System.Collections.Generic;

namespace Library.Optionals
{
    public class Query
    {
        public string HelloWorld() => "Hello, world!";

        public List<string> GetAllBooks()
        {
            // Lógica para obtener todos los libros
            return new List<string> { "Confianza total para tus hijos", "Hago lo que puedo", "Geneacion Z", "Like the old times"};
        }
    }
}