using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {

                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname)
                              && x.Playlist.UserName.Equals(username)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };

                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here

                //there are two default data types from linq given to 
                // the datatype var : IEnumerable or IQueryable

                Playlist exists = (from x in context.Playlists
                                     where x.Name.Equals(playlistname) && x.UserName.Equals(username)
                                     select x).FirstOrDefault();
                PlaylistTrack newtrack = null;
                // this list is required for  use by the BusinessRuleException of the MessageUserControl
                List<string> reasons = new List<string>(); 

                int tracknumber = 0;
                //Determine if the playlist ("parent") instance needs to be created.
                if (exists == null)
                {
                    //create a new playlist
                    exists.Name = playlistname;
                    exists.UserName = username;
                    // the .Add(item) ONLY stages your record for adding to the database
                    // the actual phyiscal add to the database in done on .SaveChanges()
                    // the return data instance from the Add will happen when 
                    // the .SaveChanges() is actually executed.
                    // thus there is a logic delay on this code.
                    // there is no real IDENTITY in the returned instance
                    // IF YOU ACCESS THE INSTANCE YOUR PKEY VALUE WILL BE 0.
                    exists = context.Playlists.Add(exists);

                    //since this is a new playlist, logically the tracknumber will be 1.
                    tracknumber = 1;
                    
                }
                else
                {
                    // Calculate the next track number for an existing playlist.
                    tracknumber = exists.PlaylistTracks.Count() + 1;

                    //RESTRICTION (BusinessRule) 
                    //BusinessRule Requires All to be in a single List<string>
                    // A Track may only exists on a playlist once.

                    newtrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if(newtrack != null)
                    {
                        reasons.Add("Track Already Exists on playlist.");
                    }
                    
                    //do we add the track to the playlist
                    if(reasons.Count() > 0 )
                    {
                        //some business rule within the transaction has failed.
                        //throw the business rule exception.
                        throw new BusinessRuleException("Adding Track to Playlist.", reasons);
                    }
                    else
                    {
                        //Part Two
                        //adding the new track to the PlaylistTracks table.
                    }

                }
             
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
               //code to go here


            }
        }//eom


    }
}
