<Query Kind="Program">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var results = BLL_Query();
	
	results.Dump();
	//to dump/display the contents of a variable in LINQpad,
	//you will use the LINQpad method .Dump

}

// Define other methods and classes here


public IEnumerable<AlbumArtists> BLL_Query()
{
	var results = from a in Albums
				  orderby a.Title
				  select new AlbumArtists
					{
						Title = a.Title,
						Year = a.ReleaseYear,
						AritstName = a.Artist.Name
					};	
	return results;			
		
}

/*
	The Query using the class is pulling from 
	multiple tables and is a subset of those tables
	the resulting dataset is NOT an Entity
	
	the Query contains ONLY primitive data values.
	the Query has NO data structures(ie: list, arrays, ...)
	
	classes that are not entities and have NO structure 
	will be referred as POCO classes
	
*/

public class AlbumArtists
{
	public string Title {get; set;}
	public int Year {get; set;}
	public string AritstName {get;set;}
}
