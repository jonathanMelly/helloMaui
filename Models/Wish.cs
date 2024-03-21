namespace HelloMaui1.Models
{
    public class Wish
    {
        public int Id { get; set; }
        public string? Definition { get; set; }
        public DateTime AccomplishedDate { get; set; }

        public override string ToString()
        {
            return $"[Wish {Id}]";
        }

    }
}
