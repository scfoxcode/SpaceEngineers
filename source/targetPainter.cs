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
        // Find working block with name
        public Type FindBlock<Type>(string name) where Type: class, IMyTerminalBlock
        {
            List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.SearchBlocksOfName(name, blocks); 
            for(var i=0; i<blocks.Count; i++)
            {
                if(blocks[i].IsWorking)
                {
                    return blocks[i] as Type;
                }
            } 
            return null;
        }

        // Save co-ordinates
        public void SaveCoords(MyDetectedEntityInfo data, IMyTextPanel display)
        {
            string result = "GPS:PAINTED:";
            Vector3D? pos = data.HitPosition;
            if(pos == null)
            {
                pos = data.BoundingBox.Center;
            }
            string position = Math.Round(pos.Value.X, 2).ToString() + ":" +
                              Math.Round(pos.Value.Y, 2) + ":" + 
                              Math.Round(pos.Value.Z, 2) + ":";

            result += position + "\n";
            Storage += result;

            display.WritePublicText(Storage, false);
        }

        // Start program
        public void Main(string argument)
        {
            int SCAN_DISTANCE = 1000;    // Distance we can scan
            int SCAN_DISTANCE_MIN = 100; // Smallest acceptable distance

            // Find a working target painter
            IMyCameraBlock targetPainter = FindBlock<IMyCameraBlock>("targetPainter_Camera");
            if(targetPainter == null)
            {
                return; // Couldn't find target painter
            }
            targetPainter.EnableRaycast = true;

            // Find a working display
            IMyTextPanel panel = FindBlock<IMyTextPanel>("targetPainter_Display");
            if(panel == null)
            {
                return; // Couldn't find a display
            }

            // Try to scan at max distance but settle for smaller value 
            MyDetectedEntityInfo data; 
            while(SCAN_DISTANCE > SCAN_DISTANCE_MIN)
            {
                if(targetPainter.CanScan(SCAN_DISTANCE))
                {
                    data = targetPainter.Raycast(SCAN_DISTANCE, 0, 0);
                    SaveCoords(data, panel);
                    return;
                }
                SCAN_DISTANCE/=2;
            }
        }  
        //to this comment.
#region post-script
    }
}
#endregion