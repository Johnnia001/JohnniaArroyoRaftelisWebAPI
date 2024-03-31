using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public interface IParcelService
{
    IEnumerable<Parcel> GetParcels();
    IEnumerable<Parcel> GetParcelsSortedByStreet();
    IEnumerable<Parcel> GetParcelsSortedByFirstName();
}

public class ParcelService : IParcelService
{
    private readonly string _filePath = "./Data/Parcels.txt";

    public IEnumerable<Parcel> GetParcels()
    {
        return ReadParcelsFromFile();
    }

    public IEnumerable<Parcel> GetParcelsSortedByStreet()
    {
        return ReadParcelsFromFile().OrderBy(p => p.Address.Split(' ')[1])
                                     .ThenBy(p => int.Parse(p.Address.Split(' ')[0]));
    }

    public IEnumerable<Parcel> GetParcelsSortedByFirstName()
    {
        return ReadParcelsFromFile().OrderBy(p =>
        {
            string[] nameParts = p.Owner?.Split(',');
            return nameParts?.Length > 1 ? nameParts[1].Trim().Split(' ')[0] : string.Empty;
        });
    }

    private IEnumerable<Parcel> ReadParcelsFromFile()
    {
        var parcels = new List<Parcel>();
        var lines = System.IO.File.ReadAllLines(_filePath).Skip(1); // Skip header
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            try {
                parcels.Add(new Parcel
                {
                    ParcelId = long.Parse(parts[0]),
                    Address = parts[1],
                    Owner = parts[2],
                    MarketValue = decimal.Parse(parts[3]),
                    SaleDate = DateTime.Parse(parts[4]),
                    SalePrice = decimal.Parse(parts[5]),
                    Link = parts[6],
                    GoogleMapsLink = $"https://www.google.com/maps?q={Uri.EscapeDataString(parts[1] + ", Mazama, WA")}"
                });
            } catch (Exception e ) {
                //Error parsing the record
                Console.Error.WriteLine($"Error parsing line: {line}");
            }
            
        }
        return parcels;
    }
}
