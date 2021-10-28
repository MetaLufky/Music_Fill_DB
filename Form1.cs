using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicFillDB
{
  public partial class Form1 : Form
  {
    private delegate void SafeFakeConsoleDelegate(string p_strPlaylist, string p_strMusicName, string p_strDateDl);
    private string m_strPlaylistPath;
    private Thread m_thFillData = null;

    public Form1()
    {
      InitializeComponent();

      m_strPlaylistPath = m_tbFolderPath.Text;
    }
    private void m_btnFillData_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(m_strPlaylistPath)) return;

      m_thFillData = new Thread(new ThreadStart(FillData));
      m_thFillData.IsBackground = true;
      m_thFillData.Start();
    }
    private void FillData()
    {
      var l_strarPlaylists = Directory.GetDirectories(m_strPlaylistPath);

      foreach (var l_strCurrentPlaylist in l_strarPlaylists)
      {
        var files = Directory.GetFiles(l_strCurrentPlaylist);
        //if (playlist == @"P:\Music\Playlist\A download asap") continue;
        foreach (var pathFile in files)
        {
          var l_flFileInfo = new FileInfo(pathFile);
          var l_strPlaylistPath = l_flFileInfo.DirectoryName;
          var l_strPlaylistPathSplitted = l_strPlaylistPath.Split('\\');
          var l_strPlaylistName = l_strPlaylistPathSplitted[l_strPlaylistPathSplitted.Length - 1];
          var l_strMusicName = Path.GetFileNameWithoutExtension(l_flFileInfo.FullName);
          var l_strExtension = Path.GetExtension(l_flFileInfo.FullName).Remove(0, 1);
          var l_dtDateMusicDl = l_flFileInfo.LastWriteTime;

          ToFakeConsole(l_strPlaylistName, l_strMusicName, l_dtDateMusicDl.ToString());

          //Playlist
          var l_nIDPlaylist = PlaylistIDRecord(l_strPlaylistName);

          if (l_nIDPlaylist == null)
          {
            var query = String.Format("INSERT INTO playlist (name_Playlist) VALUES (\"{0}\");", l_strPlaylistName);
            InsertSqlQuery(query);
          }
          l_nIDPlaylist = PlaylistIDRecord(l_strPlaylistName);

          //MetaData Music
          TagLib.File l_tlfMetaData = null;
          TimeSpan l_tsDuration = TimeSpan.Zero;
          string l_strTitle = string.Empty;
          string l_strSinger = string.Empty;
          bool l_bHasMetaData = true;
          try
          {
            l_tlfMetaData = TagLib.File.Create(pathFile);
          }
          catch (Exception ex)
          {
            l_bHasMetaData = false;
          }
          finally
          {
            if (l_tlfMetaData != null)
            {
              l_tsDuration = l_tlfMetaData.Properties.Duration;

              if (!string.IsNullOrEmpty(l_tlfMetaData.Tag.Title))
                l_strTitle = l_tlfMetaData.Tag.Title;

              if (l_tlfMetaData.Tag.Performers.Length > 0)
                l_strSinger = l_tlfMetaData.Tag.Performers[0];
            }

          }

          //Music
          var l_nIDMusic = MusicIDRecord(l_strMusicName);

          if (l_nIDMusic == null)
          {
            var dateFormated = l_dtDateMusicDl.Year.ToString() + "-" + l_dtDateMusicDl.Month.ToString() + "-" + l_dtDateMusicDl.Day.ToString();

            dateFormated += " " + l_dtDateMusicDl.Hour.ToString() + ":" + l_dtDateMusicDl.Minute.ToString() + ":" + l_dtDateMusicDl.Second.ToString();
            string query = string.Empty;
            if (l_bHasMetaData)
            {
              query = String.Format("INSERT INTO Music (name_Music, dateDl_Music, duration_Music, title_Music, uploader_Music) VALUES (\"{0}\",TIMESTAMP('{1}'), '{2}',\"{3}\",\"{4}\");", l_strMusicName, dateFormated, l_tsDuration.ToString(@"hh\:mm\:ss"), l_strTitle.Replace("\"", "\\\""), l_strSinger);
            }
            else
            {
              query = String.Format("INSERT INTO Music (name_Music, dateDl_Music) VALUES (\"{0}\",TIMESTAMP('{1}'));", l_strMusicName, dateFormated);
            }

            InsertSqlQuery(query);

            l_nIDMusic = MusicIDRecord(l_strMusicName);
          }

          //ajoute à la playlist
          if (!MusicPlaylistRelExist((int)l_nIDMusic, (int)l_nIDPlaylist))
          {
            AddMusicPlaylistRel((int)l_nIDMusic, (int)l_nIDPlaylist);
          }

          //Ajoute l'extension
          var l_nIdExtension = ExtensionIDRecord(l_strExtension);

          if (l_nIdExtension == null)
          {
            var query = String.Format("INSERT INTO Format (name_Format) VALUES (\"{0}\");", l_strExtension);
            InsertSqlQuery(query);
          }
          l_nIdExtension = ExtensionIDRecord(l_strExtension);

          //ajoute l'extention à la music
          if (!MusicFormatRelExist((int)l_nIDMusic, (int)l_nIdExtension))
          {
            AddMusicFormatRel((int)l_nIDMusic, (int)l_nIdExtension);
          }
        }
      }
    }
    private void m_btnFolderSearch_Click(object sender, EventArgs e)
    {
      using (var l_fbdMain = new FolderBrowserDialog())
      {
        DialogResult result = l_fbdMain.ShowDialog();
        //=================================================
        //TODO: Faire en sorte qu'il se rappel du dernier path qu'il a utilisé lors de la dernière utilisation
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(l_fbdMain.SelectedPath))
        {
          m_tbFolderPath.Text = l_fbdMain.SelectedPath;
          m_strPlaylistPath = l_fbdMain.SelectedPath;
        }
      }
    }
    private bool MusicFormatRelExist(int p_nMusicId, int p_nFormatId)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      string l_strQuery = "SELECT id_Rel_ForMus FROM Rel_ForMus WHERE FK_Music = " + p_nMusicId + " AND FK_Format = " + p_nFormatId + ";";

      int? l_nResult;

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(l_strQuery, cnn);

        using (MySqlDataReader l_msdrReaderSql = l_sqlcCommand.ExecuteReader())
        {
          if (l_msdrReaderSql.HasRows)
          {
            l_msdrReaderSql.Read();
            l_nResult = l_msdrReaderSql.GetInt32(0);
          }
          else
          {
            return false;
          }
        }

        cnn.Close();
      }

      return true;
    }
    private void AddMusicFormatRel(int p_nMusicId, int p_nFormatId)
    {
      var query = String.Format("INSERT INTO Rel_ForMus (FK_Music, FK_Format) VALUES (\"{0}\",\"{1}\");", p_nMusicId, p_nFormatId);
      InsertSqlQuery(query);
    }
    private static int? ExtensionIDRecord(string p_strExtension)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      string l_strQuery = "SELECT id_Format FROM Format WHERE name_Format = \"" + p_strExtension + "\";";

      int? l_nResult;

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(l_strQuery, cnn);

        using (MySqlDataReader l_msdrReaderSql = l_sqlcCommand.ExecuteReader())
        {
          if (l_msdrReaderSql.HasRows)
          {
            l_msdrReaderSql.Read();
            l_nResult = l_msdrReaderSql.GetInt32(0);
          }
          else
          {
            l_nResult = null;
          }
        }

        cnn.Close();
      }

      return l_nResult;
    }
    private static int? PlaylistIDRecord(string p_strPlaylistName)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      string l_strQuery = "SELECT id_Playlist FROM playlist WHERE name_Playlist = \"" + p_strPlaylistName + "\";";

      int? l_nResult;

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(l_strQuery, cnn);

        using (MySqlDataReader l_msdrReaderSql = l_sqlcCommand.ExecuteReader())
        {
          if (l_msdrReaderSql.HasRows)
          {
            l_msdrReaderSql.Read();
            l_nResult = l_msdrReaderSql.GetInt32(0);
          }
          else
          {
            l_nResult = null;
          }
        }

        cnn.Close();
      }

      return l_nResult;
    }
    private int? MusicIDRecord(string l_strMusicName)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      string l_strQuery = "SELECT id_Music FROM Music WHERE name_Music = \"" + l_strMusicName + "\";";

      int? l_nResult;

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(l_strQuery, cnn);

        using (MySqlDataReader l_msdrReaderSql = l_sqlcCommand.ExecuteReader())
        {
          if (l_msdrReaderSql.HasRows)
          {
            l_msdrReaderSql.Read();
            l_nResult = l_msdrReaderSql.GetInt32(0);
          }
          else
          {
            l_nResult = null;
          }
        }

        cnn.Close();
      }

      return l_nResult;
    }
    private bool MusicPlaylistRelExist(int p_nMusicId, int p_nPlaylistId)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      string l_strQuery = "SELECT id_Rel_PlaMus FROM Rel_PlaMus WHERE FK_Music = " + p_nMusicId + " AND FK_Playlist = " + p_nPlaylistId + ";";

      int? l_nResult;

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(l_strQuery, cnn);

        using (MySqlDataReader l_msdrReaderSql = l_sqlcCommand.ExecuteReader())
        {
          if (l_msdrReaderSql.HasRows)
          {
            l_msdrReaderSql.Read();
            l_nResult = l_msdrReaderSql.GetInt32(0);
          }
          else
          {
            return false;
          }
        }

        cnn.Close();
      }

      return true;
    }
    private void AddMusicPlaylistRel(int p_nMusicId, int p_nPlaylistId)
    {
      var query = String.Format("INSERT INTO Rel_PlaMus (FK_Music, FK_Playlist) VALUES (\"{0}\",\"{1}\");", p_nMusicId, p_nPlaylistId);
      InsertSqlQuery(query);
    }
    public void InsertSqlQuery(string p_strQuery)
    {
      string connetionString = "server=localhost;user=root;database=spotiHome;port=3306;password=Pa$$w0rd";

      using (MySqlConnection cnn = new MySqlConnection(connetionString))
      {
        cnn.Open();

        MySqlCommand l_sqlcCommand = new MySqlCommand(p_strQuery, cnn);

        l_sqlcCommand.ExecuteNonQuery();

        cnn.Close();
      }
    }
    private void ToFakeConsole(string p_strPlaylist, string p_strMusicName, string p_strDateDl)
    {
      if (m_tbConsole.InvokeRequired)
      {
        SafeFakeConsoleDelegate d = new SafeFakeConsoleDelegate(ToFakeConsole);
        m_tbConsole.Invoke(d, new Object[] { p_strPlaylist, p_strMusicName, p_strDateDl });
      }
      else
      {
        var l_strNewLine = p_strPlaylist + " : " + p_strMusicName + " : " + p_strDateDl + Environment.NewLine;
        m_tbConsole.Text += l_strNewLine;
        m_tbConsole.SelectionStart = m_tbConsole.TextLength;
        m_tbConsole.ScrollToCaret();
      }
    }
  }
}
