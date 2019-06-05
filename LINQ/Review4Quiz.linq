<Query Kind="Statements">
  <Connection>
    <ID>5b3e2a72-84b0-4f63-a42f-6916eac3f401</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//JOIN 3 table
from artistSide in Artists
join  albumSide in Albums
on artistSide.ArtistId equals albumSide.ArtistId
join trackSide in Tracks
on albumSide.AlbumId equals trackSide.AlbumId
select new
{
	Artist = artistSide.Name,
	Album = albumSide.Artist,
	GenreId = trackSide.AlbumId
}


//list the artists and their number of albums
// order the list from most albums by an artist to least albums by an artist

var q1 = from x in Artists 
orderby (from y in Albums where y.ArtistId == x.ArtistId select y).Count() descending
select new{
	name = x.Name,
	album = (from y in Albums where y.ArtistId == x.ArtistId select y).Count()
};

//find the maximum number of albums for all artist

var q2 = (from x in Artists
			select x.Albums.Count()).Max();
//produce a list of albums which have tracks showing their
//title, artist name, number of tracks on album and total price of
// all tracks for that album			
var q3 = (from x in Albums
			where x.Tracks.Count() > 0 
			select new {
				Title = x.Title,
				Artist = x.Artist.Name,
				Track = (from y in Tracks 
							where y.AlbumId.Equals(x.AlbumId)
							select y).Count(),
				TotalPrice = (from y in Tracks
								where y.AlbumId.Equals(x.AlbumId)
								select y.UnitPrice).Sum()
			});
q3.Dump();

	// Nested Query
	// a query within another query
	
	//List all sales support employees showing their fullname (lastname, firstname),
	//  their title and the number of customers each supports. Order by fullname.
	//  In addition show a list of the customers for each employee. For the customer
	//  list, show the fullname and phone number.

var q4 = (from x in Employees
			where x.Title.Contains("support")
			orderby x.LastName, x.FirstName
			select new
			{
				EmpName = x.LastName + "," + x.FirstName,
				EmpTitle = x.Title,
				CustomerCount = (from y in Customers 
									where y.SupportRepId.Equals(x.EmployeeId)
									select y).Count(),
				CustomerList = (from y in Customers
									where y.SupportRepId.Equals(x.EmployeeId)
									select new
									{
										CustomerName = y.LastName + "," + y.FirstName,
										CustomerPhone = y.Phone
									})
			});
			
q4.Dump();
		
	//create a list of albums showing the album title and artist name.
	//show albums with 25 or more tracks only.
	//show the songs on the album (name and length in minutes and seconds)
	//create the DTO and POCO classes

var q5 = (from x in Albums
			where x.Tracks.Count() > 25
			select new
			{
				AlbumTitle = x.Title,
				artist = x.Artist.Name,
				song = (from y in Tracks
						select new
						{
							TrackTitle = y.Name,
							Length = y.Milliseconds/60000 + ":" + (y.Milliseconds%60000)/1000
							
							
						})
			});
q5.Dump();		

// create an alphabetic list of Albums by ReleaseLabel
// Albums Missing labels will be listed as "Unknown"
var q6 = (from x in Albums
			orderby x.ReleaseLabel
			select new{
				title = x.Title,
				label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel
			});
			
q6.Dump();
