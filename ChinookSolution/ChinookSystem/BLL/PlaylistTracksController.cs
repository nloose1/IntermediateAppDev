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
            using (var context = new ChinookContext())
            {
                List<UserPlaylistTrack> results = (from x in context.PlaylistTracks
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
                                                  }).ToList();
                return results;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //trx
                //query the playlist table to see if the playlist name exists
                //if not
                //  create an instance of playlist 
                //  load 
                //  add
                //  set tracknumber to 1
                //if yes
                //  query PlayListTrack fro track id 
                //  if found
                //      Yes:Throw an error
                //      No: Increment track number
                // Create an instance of PlayListTrack
                //load
                //add
                //save changes
                List<string> errors = new List<string>();
                int tracknumber = 0;
                PlaylistTrack newtrack = null;
                Playlist exists = (from x in context.Playlists
                                  where x.Name.Equals(playlistname)
                                  && x.UserName.Equals(username)
                                  select x).FirstOrDefault();
                if (exists == null)
                {
                    //new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    context.Playlists.Add(exists); //stages ONLY
                    tracknumber = 1;
                }
                else
                {
                    //playlist exists
                    newtrack = (from x in context.PlaylistTracks
                                where x.Playlist.Name.Equals(playlistname) 
                                && x.Playlist.UserName.Equals(username)
                                && x.TrackId == trackid
                                select x).FirstOrDefault();
                    if(newtrack == null)
                    {
                        //can add to playlist
                        tracknumber = (from x in context.PlaylistTracks
                                      where x.Playlist.Name.Equals(playlistname)
                                      && x.Playlist.UserName.Equals(username)
                                      select x.TrackNumber).Max();
                        tracknumber++;

                    }
                    else
                    {
                        //track alredy on playlist
                        //buisness rule states duplicate tracks not allowed
                        //violates the buisness rule

                        //throw an excetion
                        //throw new Exception("Track already on the playlist. Duplicates not allowed.");

                        //throw a Buisness rule exception
                        //collect the errors into a List<string>
                        //After all validation is done, test the collection (List<T>) for having any messages, if so throw new BusinessRuleException()
                        errors.Add("Track already on the playlist. Duplicates not allowed.");
                    }
                }

                //all validation of Playlist and PlaylistTrack is complete
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Adding a Track", errors);
                }
                else
                {
                    //create/load/add a PlaylistTrack
                    newtrack = new PlaylistTrack();

                    newtrack.TrackId = trackid;
                    newtrack.TrackNumber = tracknumber;
                    exists.PlaylistTracks.Add(newtrack); //stage ONLY, USE THE PARENT

                    context.SaveChanges(); //physical addition
                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
