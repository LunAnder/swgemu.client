using SWG.Client.Object.Templates.Data;
using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared.Tangible
{
    public class SharedCreatureObjectTemplate : SharedTangibleObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public IntegerData Gender;
        public IntegerData Niche;
        public IntegerData Species;
        public IntegerData Race;

        public VectorData<FloatData> Acceleration;
        public VectorData<FloatData> Speed;
        public VectorData<FloatData> TurnRate;

        public StringData AnimationMapFilename;

        public FloatData SlopeModAngle;
        public FloatData SlopeModPercent;
        public FloatData WaterModPercent;
        public FloatData StepHeight;
        public FloatData CollisionHeight;
        public FloatData CollisionRadius;

        public StringData MovementDatatable;

        public VectorData<BoolData> PostureAlignToTerrain;

        public FloatData SwimHeight;
        public FloatData WarpTolerance;
        public FloatData CollisionOffsetX;
        public FloatData CollisionOffsetZ;
        public FloatData CollisionLength;
        public FloatData CameraHeight;

        public List<int> HAM;

        public override void ReadFile(IFFFile File, ITemplateRepository repo)
        {
            ReadNode(File.Root, repo);
        }

        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (checkType && node.Type != "SCOT")
            {
                _Logger.Warn("Got {0} when expecting SHOT", node.Type);
                base.ReadNode(node, repo);
                return;
            }

            ParseData(node, repo, ParseNode, _Logger);
        }


        private void ParseNode(IFFFile.Node node, ITemplateRepository repo)
        {
            int offset = 0;
            var name = node.Data.ReadAsciiString(ref offset);
            switch (name)
            {
                case "gender":
                    Gender = TryParseAssign(node, ref offset, Gender);
                    break;
                case "niche":
                    Niche = TryParseAssign(node, ref offset, Niche);
                    break;
                case "species":
                    Species = TryParseAssign(node, ref offset, Species);
                    break;
                case "race":
                    Race = TryParseAssign(node, ref offset, Race);
                    break;
                case "acceleration":
                    Acceleration = TryParseAssign(node, ref offset, Acceleration);
                    break;
                case "speed":
                    Speed = TryParseAssign(node, ref offset, Speed);
                    break;
                case "turnRate":
                    TurnRate = TryParseAssign(node, ref offset, TurnRate);
                    break;
                case "animationMapFilename":
                    AnimationMapFilename = TryParseAssign(node, ref offset, AnimationMapFilename);
                    break;
                case "slopeModAngle":
                    SlopeModAngle = TryParseAssign(node, ref offset, SlopeModAngle);
                    break;
                case "slopeModPercent":
                    SlopeModPercent = TryParseAssign(node, ref offset, SlopeModPercent);
                    break;
                case "waterModPercent":
                    WaterModPercent = TryParseAssign(node, ref offset, WaterModPercent);
                    break;
                case "stepHeight":
                    StepHeight = TryParseAssign(node, ref offset, StepHeight);
                    break;
                case "collisionHeight":
                    CollisionHeight = TryParseAssign(node, ref offset, CollisionHeight);
                    break;
                case "collisionRadius":
                    CollisionRadius = TryParseAssign(node, ref offset, CollisionRadius);
                    break;
                case "movementDatatable":
                    MovementDatatable = TryParseAssign(node, ref offset, MovementDatatable);
                    break;
                case "postureAlignToTerrain":
                    PostureAlignToTerrain = TryParseAssign(node, ref offset, PostureAlignToTerrain);
                    break;
                case "swimHeight":
                    SwimHeight = TryParseAssign(node, ref offset, SwimHeight);
                    break;
                case "warpTolerance":
                    WarpTolerance = TryParseAssign(node, ref offset, WarpTolerance);
                    break;
                case "collisionOffsetX":
                    CollisionOffsetX = TryParseAssign(node, ref offset, CollisionOffsetX);
                    break;
                case "collisionOffsetZ":
                    CollisionOffsetZ = TryParseAssign(node, ref offset, CollisionOffsetZ);
                    break;
                case "collisionLength":
                    CollisionLength = TryParseAssign(node, ref offset, CollisionLength);
                    break;
                case "cameraHeight":
                    CameraHeight = TryParseAssign(node, ref offset, CameraHeight);
                    break;
                default:
                    break;
            }

        }
    }
}
