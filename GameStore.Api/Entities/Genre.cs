using System;

namespace GameStore.Api.Entities;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    /**
        sadece public string Name { get; set; } şeklinde olursa null değer olamayacağı için hata verir.
        3 yöntem ile çözülebilir.
        1- `required` public required string Name { get; set; }
        2- `optional` public string? Name { get; set; }
        3- `varsayılan olarak boş string atanarak` public string Name { get; set; } = string.Empty;
    */
}
