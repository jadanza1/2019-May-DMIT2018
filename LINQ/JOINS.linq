<Query Kind="Expression">
  <Connection>
    <ID>c1a07453-b2f4-488e-9263-4e75afa78a5c</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
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
//OUTER JOIN