﻿using JetBrains.Annotations;
using System.IO;
using Unity.Platforms.UI;
using UnityEngine.UIElements;

namespace Unity.Build.Editor
{
    [UsedImplicitly]
    sealed class DirectoryInfoInspector : PropertyInspector<DirectoryInfo>
    {
        TextField m_TextField;

        public override VisualElement Build()
        {
            m_TextField = new TextField(DisplayName);
            m_TextField.RegisterValueChangedCallback(evt =>
            {
                Target = new DirectoryInfo(evt.newValue);
            });

            return m_TextField;
        }

        public override void Update()
        {
            m_TextField.SetValueWithoutNotify(Target.GetRelativePath());
        }
    }
}
