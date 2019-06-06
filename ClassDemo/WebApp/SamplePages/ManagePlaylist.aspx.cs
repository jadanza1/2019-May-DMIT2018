using System;
using System.Collections.Generic;
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
            TracksSelectionList.DataSource = null;
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
            //a.) playlist name
            //b.) playlist set of tracks
            //c.) have selected a track (only one tracks)
            //d.) cannot be last tracks.
            //otherwise move the selected item.

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Move Track", "Missing Playlist Name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Move Track", "Missing Playlist.");
                }
                else
                {
                    //there will be data to collect.
                    //create local variables to collect data.
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistTrackSelection = null;

                    //traverse the gridview row by row
                    // and determine which checkbox(es) are turned on.

                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control the playlist row that is the checkbox (pointer to checkbox).
                        playlistTrackSelection =
                            PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on?
                        if (playlistTrackSelection.Checked)
                        {
                            rowselected++; //count number of checkboxes turned on
                            //save the trackId
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Move Track", "Only one track be selected for movement.");
                    }
                    else
                    {
                        //is it the last track
                        //TrackNumber is a naturela count value
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Move Track", "Selected Track cannot be moved down.");
                        }
                        else
                        {
                            //move the track since it passed all the validation test.
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }

                }// eof playlistcount

            }//eof playlistName

        }//End Of Method

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            //validation
            //a.) playlist name
            //b.) playlist set of tracks
            //c.) have selected a track (only one tracks)
            //d.) cannot be first tracks.
            //otherwise move the selected item.

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Move Track", "Missing Playlist Name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Move Track", "Missing Playlist.");
                }
                else
                {
                    //there will be data to collect.
                    //create local variables to collect data.
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistTrackSelection = null;

                    //traverse the gridview row by row
                    // and determine which checkbox(es) are turned on.

                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control the playlist row that is the checkbox (pointer to checkbox).
                        playlistTrackSelection =
                            PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on?
                        if (playlistTrackSelection.Checked)
                        {
                            rowselected++; //count number of checkboxes turned on
                            //save the trackId
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Move Track", "Only one track be selected for movement.");
                    }
                    else
                    {
                        //is it the first track
                        //TrackNumber is a naturela count value
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Move Track", "Selected Track cannot be moved up.");
                        }
                        else
                        {
                            //move the track since it passed all the validation test.
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }//eom up or Down

                   

                }// eof playlistcount

            }//eof playlistName

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("hansenB", PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                PlayList.DataSource = datainfo;
                PlayList.DataBind();
            });

            
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
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
                MessageUserControl.TryRun(() => {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackId);
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                },"Adding A Track", "Track Has Been Added to the Play List.");
            }

        }

    }
}