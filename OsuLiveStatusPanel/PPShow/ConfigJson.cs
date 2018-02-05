﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsuLiveStatusPanel
{
    public class OutputConfig
    {
        public string output_format;
        public string output_file;
    }

    public class Config
    {
        public static Config Instance;

        public string oppai = "oppai.exe";
        public List<OutputConfig> output_list = new List<OutputConfig>();
        public List<OutputConfig> listen_list = new List<OutputConfig>();

        private Config() { }
        
        public static void LoadPPShowConfig(string config_path)
        {
            string config_json = File.ReadAllText(config_path);

            try
            {
                Instance = JsonConvert.DeserializeObject<Config>(config_json);
            }
            catch (Exception e)
            {
                Sync.Tools.IO.CurrentIO.WriteColor($"JsonConvert::DeserializeObject Error,{e.Message}",ConsoleColor.Red);
            }
        }

        public static void CreateDefaultPPShowConfig(string config_path)
        {
            //init
            Config default_config = new Config();
            #region Default Output List

            default_config.output_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\PP.txt",
                output_format = "92%:${pp:92.00%}pp 94%:${pp:94.00%}pp 96%:${pp:96.00%}pp 98%:${pp:98.00%}pp 100%:${pp:100.00%}pp"
            });

            default_config.output_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\map_info.txt",
                output_format = "CS:${cs} \nAR:${ar} \nOD:${od} \nHP:${hp} \n \nStars:${stars}* \nAim:${aim_stars}* \nSpeed:${speed_stars}* \n \nMaxCombo:${max_combo}"
            });
            
            default_config.output_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\mods.txt",
                output_format = "Mods:${mods}"
            });

            default_config.output_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\current_playing.txt",
                output_format = "CurrentPlaying:${artist_avaliable} - ${title_avaliable} [${version}]"
            });

            default_config.output_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\current_playing_map_info.txt",
                output_format = "Creator:${creator} \t Link:${beatmap_link}"
            });

            #endregion

            #region Default (NowPlaying) Listen List

            default_config.listen_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\current_playing.txt",
                output_format = "选图中 >///<"
            });

            default_config.listen_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\current_playing_map_info.txt",
                output_format = "推荐铺面可按以下格式发送弹幕:"
            });

            default_config.listen_list.Add(new OutputConfig()
            {
                output_file = "..\\output\\PP.txt",
                output_format = "\"?suggest -b 铺面ID号码\" ,或者\"?suggest -s 铺面SetID号码\""
            });

            #endregion

            //
            File.WriteAllText(config_path, JsonConvert.SerializeObject(default_config, Formatting.Indented));
        }
    }
}
