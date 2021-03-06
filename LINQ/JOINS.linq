<Query Kind="Expression">
  <Connection>
    <ID>5b3e2a72-84b0-4f63-a42f-6916eac3f401</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//joins can be used where navigational properties DO NOT exists
//joins can be used between a associated entities 
//	 such as :  secario pkey = fkey
//		if a relationship exists that is  (fkey/pkey) then navigational properties
//		will create an inner join.
//So, what about the out joins ex: all of x and matching of y

//left side of the join should be the support data
//right side of the join is the record collection to be procesed

//create a list of all artists and their albums
//Show the Album Title, ReleaseYear, Label (unknown if no label),
//ArtistName and Number of Tracks for each Album

//INNER JOIN between two tables assuming that there was
// no relationship in the ERD (no pkey/fkey setup)

from xRightSide  in Artists
join yLeftSide in Albums
on xRightSide.ArtistId equals yLeftSide.ArtistId 
select new
{
	title = yLeftSide.Title,
	year = yLeftSide.ReleaseYear,
	label = yLeftSide.ReleaseLabel == null ? "Unknown " : yLeftSide.ReleaseLabel,
	artist = xRightSide.Name,
	trackCount = yLeftSide.Tracks.Count()
}
//OUTER JOIN between two tables
// use a group to hold the result of the join
//use .DefaultIfempty() to handle the missing data of the joins

from xRightSide  in Artists
join yLeftSide in Albums
on xRightSide.ArtistId equals yLeftSide.ArtistId into gTemp
from p in gTemp.DefaultIfEmpty()
select new
{
	artist = xRightSide.Name,
	title = p.Title ==  null ? "" : p.Title,
	year = p.ReleaseYear == null ? "" : p.ReleaseYear.ToString(),
	label = p.ReleaseLabel == null ? "Unknown " : p.ReleaseLabel,
	trackCount = p.Title == null ? 0 : p.Tracks.Count()
	
	// OR 
	// 	trackCount = p.Tracks.Count()
}


from xRightSide in Artists
join yLeftSide in Albums
on xRightSide.ArtistId equals yLeftSide.ArtistId into gTemp
from p in gTemp.DefaultIfEmpty()
select new
{
	artist = xRightSide.Name,
	title = p.Title == null ?  "" : p.Title,
	year = p.ReleaseYear == null ? "" : p.ReleaseYear.ToString(),
	label = p.ReleaseLabel == null? "Unknown" : p.ReleaseLabel,
	trackCount = p.Title == null ? 0 : p.Tracks.Count()
}

//join 3 table Experiment 
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


//var or = from xRightSide in Orders 	
//		join yLeftSide in Stores 
//		on xRightSide.StoreID equals yLeftSide.StoreID 
//		join zRightSide in Pickers on yLeftSide.StoreID equals zRightSide.StoreID into gTemp
//		select zRightSide ;
