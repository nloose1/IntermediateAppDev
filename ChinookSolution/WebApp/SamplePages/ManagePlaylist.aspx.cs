﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
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

            if (String.IsNullOrEmpty(ArtistName.Text))
            {
                //message to the user
                MessageUserControl.ShowInfo("Entry Error", "Supply an artist name or part of");
            }
            else
            {
                TracksBy.Text = "Artist";
                SearchArg.Text = ArtistName.Text;
                TracksSelectionList.DataBind();
            }

        }


        protected void MediaTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MediaTypeDDL.SelectedIndex == 0)
            {
                //message to the user
                MessageUserControl.ShowInfo("Selection Error", "Select a media type");
            }
            else
            {
                TracksBy.Text = "MediaType";
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }
        }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Genre";
            SearchArg.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(AlbumTitle.Text))
            {
                //message to the user
                MessageUserControl.ShowInfo("Entry Error", "Supply an album title or part of");
            }
            else
            {
                TracksBy.Text = "Album";
                SearchArg.Text = AlbumTitle.Text;
                TracksSelectionList.DataBind();
            }

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //security is yet to be implemented 
            //this page needs to know the username of the currently logged user 
            //temporarely we will hard code the user name 
            string username = "HansenB";
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
                //Your code does not need to have a try catch
                //the try catch is inbedded within MessageUserControl
                //the syntax for executing for MessageUserControl
                //  MessageUserControl.TryRun(() => { coding block}, "Success Title","Success message");
                MessageUserControl.TryRun(( ) => 
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();

                },"Playlist","View current songs on playlist");

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
            string username = "HansenB";
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
                //Your code does not need to have a try catch
                //the try catch is inbedded within MessageUserControl
                //the syntax for executing for MessageUserControl
                //  MessageUserControl.TryRun(() => { coding block}, "Success Title","Success message");
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //e is the track id that is passed from the web page
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()));
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();

                }, "Add track to playlsit", "Track has been added to the playlist");

            }

        }

    }
}