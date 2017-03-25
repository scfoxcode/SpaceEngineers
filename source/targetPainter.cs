// Stephen Surtees Space Engineers Scripting Fun

// Setup guide at
// http://www.spaceengineerswiki.com/Visual_Studio_Setup_Guide

/*---------------------------------------------------------------------------*\
                              TARGET PAINTER SCRIPT

DESCRIPTION: Obtains GPS co-ordinates of a target using camera raycast.
Co-ordinates are saved onto LCD screen for later use

BLOCKS:
- Camera:    Must be named "targetPainter_Camera"
- LCD Panel: Must be named "targetPainter_Display"
-
\*---------------------------------------------------------------------------*/ 



#region pre-script
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
 
namespace IngameScript
{
    public class targetPainter : MyGridProgram
    {
#endregion
        //To put your code in a PB copy from this comment...
        public void Init()
        {
 
        }
 
        public void Save()
        {
 
        }
 
        public void Main(string argument)
        {
            Init();
        }
        //to this comment.
#region post-script
    }
}
#endregion