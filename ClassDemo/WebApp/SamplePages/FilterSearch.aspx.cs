using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
#endregion

namespace WebApp.SamplePages
{
    public partial class FilterSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindArtistList();
            }

        }

        protected void BindArtistList()
        {
            MessageUserControl.TryRun(() => {
                ArtistController sysmgr = new ArtistController();
                List<Artist> info = sysmgr.Artist_List();
                info.Sort((x, y) => x.Name.CompareTo(y.Name));
                ArtistList.DataSource = info;
                ArtistList.DataTextField = nameof(Artist.Name);
                ArtistList.DataValueField = nameof(Artist.ArtistId);
                ArtistList.DataBind();
                //ArtistList.Items.Insert(0, "Select...");
            });
        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //access data located on the gridview row
            //template web control access syntax
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            string albumId = (agvrow.FindControl("AlbumId") as Label).Text;
            //albumId = "-1"; //testing to cause an error. REMOVE/COMMENT When Finished Testing.

            MessageUserControl.TryRun(() => {
                //standard Lookup
                AlbumController sysmgr = new AlbumController();
                Album datainfo = sysmgr.Album_Get(int.Parse(albumId));
                if(datainfo == null)
                {
                    ClearControls();
                    throw new Exception("No Record Found For Selected Album. Refresh Your List of Album");
                }
                else
                {
               
                    EditAlbumID.Text = datainfo.AlbumId.ToString();
                    EditTitleOfAlbum.Text = datainfo.Title;
                    EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                    EditYear.Text = datainfo.ReleaseYear.ToString();
                    EditReleaseLabel.Text = datainfo.ReleaseLabel == null ? "" : datainfo.ReleaseLabel;

                }
            }, "Album Search", "View Album Details");

        }

        protected void ClearControls()
        {
            EditAlbumID.Text = string.Empty;
            EditTitleOfAlbum.Text = string.Empty;
            //EditAlbumArtistList.SelectedValue = "0";
            EditYear.Text = string.Empty;
            EditReleaseLabel.Text = string.Empty;
        }

        protected void Add_Button_Click(object sender, EventArgs e)
        {
            Album newAlbum = BuildAlbumFromUserInput();
           if(IsValid)
            {
                MessageUserControl.TryRun(() =>
                {
                    var controller = new AlbumController();
                    int newAlbumId = controller.Album_Add(newAlbum);

                    //EditAlbumArtistList.SelectedValue = newAlbumId.ToString(); //Might not be necessary. Double Check
                    EditAlbumID.Text = newAlbumId.ToString();
                },"Album Add", "Album Added Successfully");
            }
        }

        protected void Update_Button_Click(object sender, EventArgs e)
        {
            int id;
            if(IsValid && int.TryParse(EditAlbumID.Text, out id))
            {
                MessageUserControl.TryRun(() =>
                {
                    Album updateAlbum = BuildAlbumFromUserInput();
                    updateAlbum.AlbumId = id;
                    var controller = new AlbumController();
                    int updatedAlbumId = controller.Album_Update(updateAlbum);

                    //EditAlbumArtistList.SelectedValue = newAlbumId.ToString(); //Might not be necessary. Double Check
                    EditAlbumID.Text = updateAlbum.ToString();
                }, "Album Update", "Album Updated Successfully");
            }
        }

        protected void Remove_Button_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(EditAlbumID.Text, out id))
            {
                MessageUserControl.TryRun(() =>
                {
                    var controller = new AlbumController();
                     controller.Album_Delete(id);
                    ClearControls();
                    //EditAlbumArtistList.SelectedValue = newAlbumId.ToString(); //Might not be necessary. Double Check
                }, "Album Remove", "Album Removed Successfully");
            }
        }

        private Album BuildAlbumFromUserInput()
        {
            bool customIsValid = true;
            Album item = new Album();

            item.Title = EditTitleOfAlbum.Text;
            if (EditAlbumArtistList.SelectedIndex == -1) //Might need to be changed when empty artist field is added.
            {
                customIsValid = false;
                throw new Exception("Please Select an Artist");
            }
            else
            {
                item.ArtistId = int.Parse(EditAlbumArtistList.SelectedValue);
                //throw new Exception("The selected artist ID is" + EditAlbumArtistList.SelectedValue);
            }
            item.ReleaseYear = int.Parse(EditYear.Text); // maybe change to just int in the textbox
            item.ReleaseLabel = EditReleaseLabel.Text;

            return item;
        }

    }
}