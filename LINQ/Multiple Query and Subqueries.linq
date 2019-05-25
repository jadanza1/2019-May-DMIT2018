<Query Kind="Program">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Nested Query
	// a query within another query
	
	//List all sales support employees showing their fullname(lastname, firstname),
	// their title and the number of customers each suppors. Order by fullname.
	// In addition, show a list of the customers for each employee.
//	
//	empfrom emp in Employees
//	where emp.Title.Contains("Support")
//	orderby emp.LastName, emp.FirstName
//	select new SupportEmployee
//	{
//		Name = emp.LastName + ", " + emp.FirstName,
//		EmpTitle = emp.Title,
//		ClientCount = emp.SupportRepIdCustomers.Count(),
//		ClientList = (from cus in emp.SupportRepIdCustomers
//						orderby cus.LastName, cus.FirstName
//						select new
//							{
//								Name = cus.LastName + ", " + cus.LastName,
//								Phone = cus.Phone
//							}).ToList()
//						
//	};
//emplist.Dump();

//create a list of albums showing the album title and artist name
//show albums with 25 or more tracks only.
//show the songs on the album (name and length in minutes and seconds)


var albumInfo = from al in Albums
				where al.Tracks.Count() >= 25
				select new AlbumSongs{
					albumTitle = al.Title,
					albumName = al.Artist.Name,
					Songs = ( from y in al.Tracks
								select new Song
								{
									Songtitle = y.Name,
									SongLength = y.Milliseconds/60000 + ":" + 
													(y.Milliseconds%60000)/1000
								}).ToList()
				};
albumInfo.Dump();
				
		
}

Albums
// Define other methods and classes here


//POCO: flat record, no structure (collection)

public class Client
{
	public string Name {get;set;}
	public string Phone {get; set;}
}

public class Song
{
	public string SongTitle{get; set;}
	public string SongLength{get; set;}
	
}

public class AlbumSongs
{
	public string ATitle {get;set;}
	public string AName {get;set;}
	public List<Song> Songs{get;set;}
}

//DTO: a class which contains flat data AND Structure (collection)

public class SupportEmployee
{
	public string Name {get; set;}
	public string EmpTitle {get; set;}
	public int ClientCount {get; set;}
	public List<Client> ClientList {get; set;}
}




