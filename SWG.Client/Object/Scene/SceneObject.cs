using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Object.Scene.Variables;
using SWG.Client.Object.Templates.Shared.Tangible;
using UnityEngine;

namespace SWG.Client.Object.Scene
{
    public class SceneObject
    {
        public long ObjectId { get; set; }

        public string DisplayName { get; set; }

        public string DetailedDescription { get; set; }

        public SharedCreatureObjectTemplate TemplateObject { get; set; }

        public StringId ObjectName { get; set; }

        public Quaternion Direction { get; set; }

        public Vector3 WorldPosition { get; set; }

        public Matrix4x4 TransformCollisionMatrix { get; set; }

        public int ObjectCRC { get; set; }

        public int PlanetCRC { get; set; }

        public GameObjectType GameObjectType { get; set; }

        public float DirectionAngle { get; set; }

        public float SpectialDirectionAngle { get; set; }

        public List<SceneObject> Children { get; set; } 


    }

}
