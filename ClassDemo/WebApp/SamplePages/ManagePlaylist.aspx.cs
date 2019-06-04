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
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
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
                },"Adding A Track", "Track Has Been Added to the Play List.");
            }

        }

    }
}