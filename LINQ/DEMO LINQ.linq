<Query Kind="Expression">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//DEMO FOR LINQ


// ' => ' means "Do the following: / lamda"
//Using Query Syntax, List All the Records from the Albums table
//from x in Albums
//select x

//Using Method Syntax, List All the Records from the Albums table
//Albums.Select (x => x)

//Using Query Syntaxt, List All the Records  from the Albums table for ArtistID 1
//from x in Albums
//where x.ArtistId == 1  
//select x

//Using Method Syntax, List all the Records from the Albums Table from ArtistID 1
//Albums.Where (x => (x.ArtistId == 1))

//Using Query Syntax, List all the records from the Albums table for ArtistID 1 Ordered by ascending Release Year
//from x in Albums
//where x.ArtistId == 1  
//orderby x.ReleaseYear
//select x

//Using Method Syntax, List all the records from the Albums table for ArtistID 1 Ordered by ascending Release Year
//Albums.Where (x => (x.ArtistId == 1)).OrderBy (x => x.ReleaseYear)

/* Multiple Column Sorting Ex1
	//List all the records from the entity Albums Ordered by descending Release Year 
	  and alphabetically by Title
		descending is required
		ascending is the default for any sort item/column
		//Query Syntax
			from x in Albums
			orderby x.ReleaseYear descending, x.Title ascending
			select x

		//Method Syntax
			Albums
			   .OrderByDescending (x => x.ReleaseYear)
			   .ThenBy (x => x.Title)
*/

/* Repeat the previous query but only for years 2007 thru 2010 inclusive
	//Query Syntax
		 	from x in Albums
			orderby x.ReleaseYear descending, x.Title ascending
			where x.ReleaseYear <= 2010 && x.ReleaseYear >= 2007
			select x
	//Method Syntax
		 Albums
	   		.OrderByDescending (x => x.ReleaseYear)
	   		.ThenBy (x => x.Title)
	  		 .Where (x => ((x.ReleaseYear <= 2010) && (x.ReleaseYear >= 2007)))
*/

/* List all customers in Alphabetical order by lastname, firstname who live in the US
	with a yahoo email.
	//Query Syntax
			from x in Customers
			where x.Country == "USA" && x.Email.Contains("@yahoo.com") 
			orderby x.LastName, x.FirstName
			select x
	//Method Syntax
		.Where(c => (c.Country.Equals("USA") && c.Email.Contains("yahoo")))
			.OrderBy( c => (c.LastName))
			.ThenBy(c => (c.LastName))
*/


/*	one can query for a subset of entity attributes,
	one can query for a set of attributes from multiple entities.
	
	create a query to return ONLY the TrackId and song name for use 
	by a dropdownlist.
	
	from t in Tracks
	orderby t.Name
	select new 
	{
		TrackId =  t.TrackId,	// or you can use any names you want
		Song = t.Name
	}
*/

/* create an alphabetical list of Albums showing the album title 
	ReleaseYear and ArtistName
	
	//My Whack Query Super 
		from a in Albums
		orderby a.Title
		select new
		{
			Title = a.Title,
			ReleaseYear = a.ReleaseYear,
			ArtistName = from x in Artists
						where x.ArtistId == a.ArtistIds
						select x.Name 
		};

		//Proper Query

		from a in Albums
		orderby a.Title
		select new
		{
			Title = a.Title,
			Year = a.ReleaseYear,
			AritstName = a.Artist.Name
		}


*/





	
		