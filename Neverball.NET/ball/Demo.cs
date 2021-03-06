﻿//  The original source code has been ported to .NET with deep assistance by AltSoftLab in 2015-2016
//  The solution source code based on and requires AltSDK (visit http://www.AltSoftLab.com/ for more info),
//  and is provided "as is" without express or implied warranty of any kind.
//
//  The solution can still require several optimizations: some OpenGL display lists has been removed and
//  render logic changed to be more transparent and be possible to port to other render engines (maybe
//  MonoGame or Unity). Also vector arrays can be used for positions, texture coords & colors. Audio is
//  not implemented directly, but all sound calls directed to Audio class. Game menu ported partly.
//
//  Thanks so much to AltSoftLab for help!
//
//  AltSoftLab on Facebook      - http://www.facebook.com/AltSoftLab
//  AltSoftLab on Twitter       - http://www.twitter.com/AltSoftLab
//  AltSoftLab on Instagram     - http://www.instagram.com/AltSoftLab
//  AltSoftLab on Unity forums  - http://forum.unity3d.com/threads/335966
//  AltSoftLab website          - http://www.AltSoftLab.com


using System;

using Alt.Collections.Generic;
using Alt.Sketch;


namespace Neverball.NET
{
    class Demo
    {
        public string player;
        public Int64 date;

        public int mode;

        public string shot;   /* image filename */
        public string file;   /* level filename */

        public int time;            /* time limit        */
        public int goal;            /* coin limit        */
        public int goal_e;          /* goal enabled flag */
        public int score;           /* total coins       */
        public int balls;           /* number of balls   */
        public int times;           /* total time        */


        internal static int demo_play_init(string name, Level level, MODE mode, int t, int g, int e, int s, int b, int tt)
        {
            Demo demo = new Demo();

            demo.mode = (int)mode;

            System.TimeSpan ts = System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1));
            demo.date = (System.Int64)ts.TotalSeconds;

            demo.player = Config.config_get_s(Config.CONFIG_PLAYER);

            demo.shot = level.shot;
            demo.file = level.file;

            demo.time = t;
            demo.goal = g;
            demo.goal_e = e;
            demo.score = s;
            demo.balls = b;
            demo.times = tt;

            Audio.audio_music_fade_to(2.0f, level.song);

            /*
             * Init both client and server, then process the first batch
             * of commands generated by the server to sync client to
             * server.
             */

            if (game_client.game_client_init(level.file) != 0 &&
                game_server.game_server_init(level.file, t, e) != 0)
            {
                game_client.game_client_step();
                return 1;
            }

            return 0;
        }
    }
}
