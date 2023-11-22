using SQLite;


public class People
    {

        [PrimaryKey, AutoIncrement]

    public int Id { get; set; }
    public string Name { get; set; }
    public int Phone { get; set; }

    public int Department { get; set; }
    public string AddressStreet { get; set; }

    public string AddressCity { get; set; }

    public string AddressState { get; set; }
    public int AddressZIP { get; set; }
    public string AddressCountry { get; set; }


    }

