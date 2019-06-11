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
                //there are two default datatypes from Linq given to
                //the datatype var: IEnumerable or IQueryable
                //in C# explicit datatypes are created at compile time
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                   && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;

                //this list is required for use by the BusinessRuleException of the
                //   MessageUserControl
                List<string> reasons = new List<string>();

                //determine if the playlist ("parent") instance needs to be created
                if (exists == null)
                {
                    //create a new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    //the .Add(item) ONLY stages your record for adding to the database
                    //the actual physical add to the database is done on .SaveChanges()
                    //the returned data instance from the add will happen when
                    //  the .SaveChanges is actually executed
                    //thus there is a logic delay on this code
                    //there is NO REAL IDENTITY value in the returned instance
                    //IF YOU access the instance your PKEY value WILL BE ZERO!!!!
                    exists = context.Playlists.Add(exists);
                    //since this is a new playlist
                    //logically the tracknumber will be 1
                    tracknumber = 1;
                }
                else
                {
                    //calculate the next track number for an existing playlist
                    tracknumber = exists.PlaylistTracks.Count() + 1;

                    //restriction (BusinessRule)
                    //BusinessRule requires all errors to be in a single List<string>
                    //A track may only exist on a playlist once
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if (newTrack != null)
                    {
                        reasons.Add("Track already exists on playlist");
                    }
                }

                //do we add the track to the playlist
                if (reasons.Count() > 0)
                {
                    //some business rule within the transactin has failed
                    //throw the BusinessRuleException
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                }
                else
                {
                    //Part Two
                    //adding the new track to the PlaylistTracks table.

                    //create a new instance of the PlaylistTrack
                    newTrack = new PlaylistTrack();
                    //load with known data
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;

                    //currently the playlistid is unknown if the
                    //playlist is brand new
                    //NOTE: using navigational properties, one can let
                    //      the HashSet of Playlist handle the PlaylistId
                    //      pkey value
                    //adding via the navigational property will have the
                    //      system enter the parent pkey for the 
                    //      coresponding fkey value
                    //In PlaylistTrack, PlaylistId is BOTH, the pkey and fkey
                    //During SaveChanges() the PlaylistId will be filled
                    exists.PlaylistTracks.Add(newTrack);

                    //committing of your work
                    //only one comit for the transaction
                    //During .SaveChanges() the data is added physically to
                    //    your database at which time pkey (identity) is 
                    //    generated
                    //the order of actions has been done by your logic
                    //the Playlist pkey will be generated
                    //this value will be placed in the fkey of the child record
                    //the child record will be placed in its table
                    context.SaveChanges();
                }
            }
        }//eom

        //public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        //{
        //    using (var context = new ChinookSystemContext())
        //    {
        //        //code to go here

        //        //there are two default data types from linq given to 
        //        // the datatype var : IEnumerable or IQueryable

        //        Playlist exists = (from x in context.Playlists
        //                             where x.Name.Equals(playlistname) && x.UserName.Equals(username)
        //                             select x).FirstOrDefault();
        //        PlaylistTrack newtrack = null;
        //        int tracknumber = 0;

        //        // this list is required for  use by the BusinessRuleException of the MessageUserControl
        //        List<string> reasons = new List<string>(); 

        //        //Determine if the playlist ("parent") instance needs to be created.
        //        if (exists == null)
        //        {
        //            //create a new playlist
        //            exists = new Playlist(); 
        //            exists.Name = playlistname;
        //            exists.UserName = username;
        //            // the .Add(item) ONLY stages your record for adding to the database
        //            // the actual phyiscal add to the database in done on .SaveChanges()
        //            // the return data instance from the Add will happen when 
        //            // the .SaveChanges() is actually executed.
        //            // thus there is a logic delay on this code.
        //            // there is no real IDENTITY in the returned instance
        //            // IF YOU ACCESS THE INSTANCE YOUR PKEY VALUE WILL BE 0.
        //            exists = context.Playlists.Add(exists);

        //            //since this is a new playlist, logically the tracknumber will be 1.
        //            tracknumber = 1;

        //        }
        //        else
        //        {
        //            // Calculate the next track number for an existing playlist.
        //            tracknumber = exists.PlaylistTracks.Count() + 1;

        //            //RESTRICTION (BusinessRule) 
        //            //BusinessRule Requires All to be in a single List<string>
        //            // A Track may only exists on a playlist once.

        //            newtrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
        //            if(newtrack != null)
        //            {
        //                reasons.Add("Track Already Exists on playlist.");
        //            }

        //            //do we add the track to the playlist
        //            if(reasons.Count() > 0 )
        //            {
        //                //some business rule within the transaction has failed.
        //                //throw the business rule exception.
        //                throw new BusinessRuleException("Adding Track to Playlist.", reasons);
        //            }
        //            else
        //            {
        //                //Part Two
        //                //adding the new track to the PlaylistTracks table.

        //                // create a new instance of the PlaylistTrack
        //                newtrack = new PlaylistTrack();
        //                //load with known data:
        //                newtrack.TrackId = trackid;
        //                newtrack.TrackNumber = tracknumber;

        //                //Currently, the playlist is unknown. If the playlist is brand new.
        //                //NOTE: using navigational properties, one can let
        //                //      the Hashset of Playlist handle the PlaylistId
        //                //      pkey Value.
        //                // adding via the navigational property will have the 
        //                //      system enter the parent pkey for the corresponding
        //                //      fkey value.
        //                //  In PlaylistTrack, PlaylistId is BOTH the pkey and fkey.
        //                // During the .SaveChanges(), the PlaylistId will be filled.

        //                exists.PlaylistTracks.Add(newtrack);

        //                //committing your work
        //                //only one comit for the transaction,
        //                //During the .SaveChanges(), the data is added phyisically 
        //                //   to your database at which time pkey (identity) is 
        //                //   generated.
        //                //The order of actions has been done by your logic.
        //                //The Playlist pkey will be generated
        //                //this value will be placed in the fkey of the child record.
        //                //the child record will be placed in its table.
        //                context.SaveChanges();

        //            }

        //        }

        //    }
        //}//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here 
                //since data can be accessed by multiple individuals at the same time
                //  your BLL method should do validate to ensure the data coming in is
                //  is appropriate
                var userName = username;
                var playlistname1 = playlistname;
                var trackId = trackid;
                int tracknumber1 = tracknumber;
                string direct = direction;
                var exists = context.Playlists.Where(x => x.UserName.Equals(username) &&
                                 x.Name.Equals(playlistname)).Select(x => x).FirstOrDefault();
                //playlist no longer exists
                if (exists == null)
                {
                    throw new Exception("Play list has been removed from files.");
                }
                else
                {
                    var movetrack = exists.PlaylistTracks.Where(x => x.TrackId == trackid).Select(x => x).FirstOrDefault();
                    //playlist track no longer exists
                    if (movetrack == null)
                    {
                        throw new Exception("Play list track has been removed from files. movetrack");
                    }
                    else
                    {
                        PlaylistTrack othertrack = null;
                        //determine direction
                        if (direction.Equals("up"))
                        {
                            if (movetrack.TrackNumber == 1)
                            {
                                throw new Exception("Play list track already at top.");
                            }
                            else
                            {
                                //setup for track movement
                                othertrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == movetrack.TrackNumber - 1
                                              select x).FirstOrDefault();
                                if (othertrack == null)
                                {
                                    throw new Exception("Play list tracks have been altered. Unable to complete move.");
                                }
                                else
                                {
                                    movetrack.TrackNumber -= 1;
                                    othertrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            if (movetrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Play list track already at bottom.");
                            }
                            else
                            {
                                //setup for track movement
                                othertrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == movetrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (othertrack == null)
                                {
                                    throw new Exception("Play list tracks have been altered. Unable to complete move.");
                                }
                                else
                                {
                                    movetrack.TrackNumber += 1;
                                    othertrack.TrackNumber -= 1;
                                }
                            }
                        }//eom up or down
                        //staging
                        //update
                        context.Entry(movetrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(othertrack).Property(y => y.TrackNumber).IsModified = true;
                        //commit
                        context.SaveChanges();
                    }
                }
            }
        }//eom
         //public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
         //{
         //    using (var context = new ChinookSystemContext())
         //    {
         //        //code to go here 
         //        //since data can be accessed by multiple individuals at the same time
         //        //  your BLL method should do validation to ensure data coming in is still appropriate.

        //        var exists = context.Playlists.Where(x => x.UserName.Equals(username) &&
        //                        x.Name.Equals(playlistname)).Select(x => x).FirstOrDefault();
        //        //playlist no longer exists
        //        if(exists == null)
        //        {
        //            throw new Exception("Play List has been removed from files.");
        //        }
        //        else
        //        {
        //            var movetrack = exists.PlaylistTracks.Where(x => x.TrackId == trackid).Select(x => x).FirstOrDefault();
        //            //playlist track no longer exist
        //            if(movetrack == null)
        //            {
        //                throw new Exception("Play list track has been removed from files. 1");
        //            }
        //            else
        //            {
        //                PlaylistTrack othertrack = null;
        //                //determine direction
        //                if(direction.Equals("up"))
        //                {
        //                    if (movetrack.TrackNumber == 1)
        //                    {
        //                        throw new Exception("Already at Top. Play list cannot be moved. ");
        //                    }
        //                    else
        //                    {
        //                        //setup for track movement.
        //                        othertrack = (from x in exists.PlaylistTracks
        //                                      where x.TrackNumber.Equals(movetrack.TrackNumber - 1)
        //                                      select x).FirstOrDefault();
        //                        if(othertrack == null)
        //                        {
        //                            throw new Exception("Playlist tracks have been altered. Unable to complete move.");
        //                        }
        //                        else
        //                        {
        //                            movetrack.TrackNumber -= 1;
        //                            othertrack.TrackNumber += 1;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (movetrack.TrackNumber == 1)
        //                    {
        //                        throw new Exception("Already at Bottom. Play list cannot be moved. ");
        //                    }
        //                    else
        //                    {
        //                        //setup for track movement.
        //                        othertrack = (from x in exists.PlaylistTracks
        //                                      where x.TrackNumber.Equals(movetrack.TrackNumber + 1)
        //                                      select x).FirstOrDefault();
        //                        if (othertrack == null)
        //                        {
        //                            throw new Exception("Playlist tracks have been altered. Unable to complete move.");
        //                        }
        //                        else
        //                        {
        //                            movetrack.TrackNumber += 1;
        //                            othertrack.TrackNumber -= 1;
        //                        }
        //                    }

        //                }//oem up or down
        //                    // staging
        //                    //update

        //                context.Entry(movetrack).Property(y => y.TrackNumber).IsModified = true;
        //                context.Entry(othertrack).Property(y => y.TrackNumber).IsModified = true;
        //                context.SaveChanges();
        //            }
        //        }

        //    }
        //}//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
                //code to go here
                //find playlist

                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username)
                                && x.Name.Equals(playlistname)
                              select x).FirstOrDefault();
                //check
                if (exists == null)
                {
                    //   null - msg
                    throw new Exception("Play list has been removed from the file.");
                }
                else
                {
                    //   exists
                    //     -> find tracks that will be kept
                    //        --> these tracks will be in set A but not in Set B
                    var tracksKept = exists.PlaylistTracks.Where(tr => !trackstodelete.Any(td => td == tr.TrackId)).OrderBy(tr => tr.TrackNumber);

                    //    -> Remove the unwanted tracks from the database 
                    PlaylistTrack item = null;
                    foreach (var dtrackid in trackstodelete)
                    {
                        //lookup instance(track) to delete 
                        item = exists.PlaylistTracks.Where(tr => tr.TrackId == dtrackid).Select(tr => tr).FirstOrDefault();
                        //check
                        if (item != null)
                        {
                            //      there remove
                            exists.PlaylistTracks.Remove(item);
                            //    -> Renumber the tracks that were kept by updating their 
                            //           track numbers.
                            int number = 1;
                            foreach (var tKept in tracksKept)
                            {
                                tKept.TrackNumber = number;
                                context.Entry(tKept).Property(y => y.TrackNumber).IsModified = true;
                                number++;
                            }
                        }


                    }

                    //commit your work
                    context.SaveChanges();
                }
            }
        }//eom


    }
}
