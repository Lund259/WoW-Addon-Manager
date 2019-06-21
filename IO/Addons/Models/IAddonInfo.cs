using System;

namespace IO.Addons.Models
{
    public interface IAddonInfo
    {
        string Author { get; set; }
        string Category { get; set; }
        string Description { get; set; }
        string Email { get; set; }
        Version Interface { get; set; }
        string Title { get; set; }
        Version Version { get; set; }
        Uri Website { get; set; }
    }
}