using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
//using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string customerRole = ConfigurationManager.AppSettings["customerRole"];
            TracksSelectionList.DataSource = null;
            if(Request.IsAuthenticated)
            {
                if (!User.IsInRole(customerRole))
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Text = "Artist";
            SearchArg.Text = ArtistName.Text;
            TracksSelectionList.DataBind();


        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            TracksBy.Text = "MediaType";
            SearchArg.Text = MediaTypeDDL.SelectedValue;
            TracksSelectionList.DataBind();

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Text = "Genre";
            SearchArg.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            TracksBy.Text = "Album";
            SearchArg.Text = AlbumTitle.Text;
            TracksSelectionList.DataBind();

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "Play list Name is Required to Lookup Playlist.");
            }
            else
            {
                string username = "HansenB"; //we will alter this when security is done.
                string playlistName = PlaylistName.Text;
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistName, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();

                });
            }
        }

          protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
            //validation
            //  a)playlist name
            //  b)playlist set of tracks
            //  c)have selected a track (only one track)
            //  d)cannot be last track
            //otherwise move
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Move Track", "Missing Playlist name");
            }
            else
            {
                if(PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Move Track", "Missing Playlist");
                }
                else
                {
                    //there will be data to collect
                    //create local variables to collect data
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlisttrackselection = null;

                    //traverse the GridView, row by row
                    // and determine which checkbox(es) are turn on
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control the playlist row that is
                        //    the checkbox (pointer to checkbox)
                        playlisttrackselection =
                            PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on??
                        if (playlisttrackselection.Checked)
                        {
                            rowselected++; //count number of checkboxes turned on
                            //save the trackid
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //how many tracks were selected
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Move Track", "Only one track can be selected for movement.");
                    }
                    else
                    {
                        //is it the last track
                        //TrackNumber is a natural count value
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Move Track", "Selected track cannot be moved down.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }

                }//eof playlist count
            }//eof playlistname
        }//eom

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            //validation
            //  a)playlist name
            //  b)playlist set of tracks
            //  c)have selected a track (only one track)
            //  d)cannot be 1st track
            //otherwise move
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Move Track", "Missing Playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Move Track", "Missing Playlist");
                }
                else
                {
                    //there will be data to collect
                    //create local variables to collect data
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlisttrackselection = null;

                    //traverse the GridView, row by row
                    // and determine which checkbox(es) are turn on
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control the playlist row that is
                        //    the checkbox (pointer to checkbox)
                        playlisttrackselection =
                            PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on??
                        if (playlisttrackselection.Checked)
                        {
                            rowselected++; //count number of checkboxes turned on
                            //save the trackid
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //how many tracks were selected
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Move Track", "Only one track can be selected for movement.");
                    }
                    else
                    {
                        //is it the 1st track
                        //TrackNumber is a natural count value
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Move Track", "Selected track cannot be moved up.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }

                }//eof playlist count
            }//eof playlistname
        }



        //protected void MoveDown_Click(object sender, EventArgs e)
        //{
        //    //code to go here
        //    //validation
        //    //a.) playlist name
        //    //b.) playlist set of tracks
        //    //c.) have selected a track (only one tracks)
        //    //d.) cannot be last tracks.
        //    //otherwise move the selected item.

        //    if (string.IsNullOrEmpty(PlaylistName.Text))
        //    {
        //        MessageUserControl.ShowInfo("Move Track", "Missing Playlist Name.");
        //    }
        //    else
        //    {
        //        if (PlayList.Rows.Count == 0)
        //        {
        //            MessageUserControl.ShowInfo("Move Track", "Missing Playlist.");
        //        }
        //        else
        //        {
        //            //there will be data to collect.
        //            //create local variables to collect data.
        //            int trackid = 0;
        //            int tracknumber = 0;
        //            int rowselected = 0;
        //            CheckBox playlistTrackSelection = null;

        //            //traverse the gridview row by row
        //            // and determine which checkbox(es) are turned on.

        //            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
        //            {
        //                //access the control the playlist row that is the checkbox (pointer to checkbox).
        //                playlistTrackSelection =
        //                    PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
        //                //is the checkbox on?
        //                if (playlistTrackSelection.Checked)
        //                {
        //                    rowselected++; //count number of checkboxes turned on
        //                    //save the trackId
        //                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
        //                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
        //                }
        //            }
        //            if (rowselected != 1)
        //            {
        //                MessageUserControl.ShowInfo("Move Track", "Only one track be selected for movement.");
        //            }
        //            else
        //            {
        //                //is it the last track
        //                //TrackNumber is a naturela count value
        //                if (tracknumber == PlayList.Rows.Count)
        //                {
        //                    MessageUserControl.ShowInfo("Move Track", "Selected Track cannot be moved down.");
        //                }
        //                else
        //                {
        //                    //move the track since it passed all the validation test.
        //                    MoveTrack(trackid, tracknumber, "down");
        //                }
        //            }

        //        }// eof playlistcount

        //    }//eof playlistName

        //}//End Of Method

        //protected void MoveUp_Click(object sender, EventArgs e)
        //{
        //    //code to go here
        //    //validation
        //    //a.) playlist name
        //    //b.) playlist set of tracks
        //    //c.) have selected a track (only one tracks)
        //    //d.) cannot be first tracks.
        //    //otherwise move the selected item.

        //    if (string.IsNullOrEmpty(PlaylistName.Text))
        //    {
        //        MessageUserControl.ShowInfo("Move Track", "Missing Playlist Name.");
        //    }
        //    else
        //    {
        //        if (PlayList.Rows.Count == 0)
        //        {
        //            MessageUserControl.ShowInfo("Move Track", "Missing Playlist.");
        //        }
        //        else
        //        {
        //            //there will be data to collect.
        //            //create local variables to collect data.
        //            int trackid = 0;
        //            int tracknumber = 0;
        //            int rowselected = 0;
        //            CheckBox playlistTrackSelection = null;

        //            //traverse the gridview row by row
        //            // and determine which checkbox(es) are turned on.

        //            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
        //            {
        //                //access the control the playlist row that is the checkbox (pointer to checkbox).
        //                playlistTrackSelection =
        //                    PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
        //                //is the checkbox on?
        //                if (playlistTrackSelection.Checked)
        //                {
        //                    rowselected++; //count number of checkboxes turned on
        //                    //save the trackId
        //                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
        //                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
        //                }
        //            }
        //            if (rowselected != 1)
        //            {
        //                MessageUserControl.ShowInfo("Move Track", "Only one track be selected for movement.");
        //            }
        //            else
        //            {
        //                //is it the first track
        //                //TrackNumber is a naturela count value
        //                if (tracknumber == 1)
        //                {
        //                    MessageUserControl.ShowInfo("Move Track", "Selected Track cannot be moved up.");
        //                }
        //                else
        //                {
        //                    //move the track since it passed all the validation test.
        //                    MoveTrack(trackid, tracknumber, "up");
        //                }
        //            }//eom up or Down



        //        }// eof playlistcount

        //    }//eof playlistName

        //}

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("HansenB", PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                PlayList.DataSource = datainfo;
                PlayList.DataBind();
            }, "Move Track", "Tracked has been moved");


        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Delete Tracks", "You need to look up your playlist to select tracks to delete.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Delete Tracks", "You need to look up your playlist to select tracks to delete.");
                }
                else
                {
                    //gather the data necessary for the deletion
                    List<int> trackstodelete = new List<int>();
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackstodelete.Add(int.Parse((PlayList.Rows[i].FindControl("TrackId") as Label).Text));
                        }
                        //was at least one track selected
                        if(rowselected == 0 )
                        {
                            MessageUserControl.ShowInfo("Delete Track", "You need to select at least one track to delete.");
                        }
                        else
                        {
                            MessageUserControl.TryRun(() =>
                            {
                                PlaylistTracksController sysmgr = new PlaylistTracksController();
                                sysmgr.DeleteTracks("HansenB", PlaylistName.Text, trackstodelete);
                                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                                PlayList.DataSource = datainfo;
                                PlayList.DataBind();
                            },"Delete Track", "Tracks Have Been Deleted.");
                            //pass on the data to do the delete in the BLL
                            // refresh the list

                        }

                    }
                }
            }

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "Play list Name is Required to Add to A Playlist.");
            }
            else
            {
                string playlistname = PlaylistName.Text;
                string username = "HansenB"; // change when security is implemented.
                //obtain trackID from the listview line that was selected.
                // for the line, I have created a CommandArgument
                //this value is available via the ListViewCommandEventArgs parameter e

                int trackId = int.Parse(e.CommandArgument.ToString());
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackId);
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                }, "Adding A Track", "Track Has Been Added to the Play List.");
            }

        }

    }
}