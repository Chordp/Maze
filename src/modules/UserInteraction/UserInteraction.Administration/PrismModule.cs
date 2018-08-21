﻿using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;
using Orcus.Administration.Library.Services;
using Prism.Modularity;
using Unclassified.TxLib;
using UserInteraction.Administration.Views;

namespace UserInteraction.Administration
{
    public class PrismModule : IModule
    {
        private readonly IClientCommandRegistrar _registrar;

        public PrismModule(IClientCommandRegistrar registrar)
        {
            _registrar = registrar;
        }

        public void Initialize()
        {
            Tx.LoadFromEmbeddedResource("UserInteraction.Administration.Resources.UserInteraction.Translation.txd");

            _registrar.RegisterView(typeof(MessageBoxView), "UserInteraction:MessageBox", Asd(),
                CommandCategory.Interaction);
        }

        private Viewbox Asd()
        {
            StringReader stringReader = new StringReader(
                @"<Viewbox Width=""16"" Height=""16"" xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
  <Rectangle Width=""16"" Height=""16"">
    <Rectangle.Fill>
      <DrawingBrush>
        <DrawingBrush.Drawing>
          <DrawingGroup>
            <DrawingGroup.Children>
              <GeometryDrawing Brush=""#00FFFFFF"" Geometry=""F1M16,16L0,16 0,0 16,0z"" />
              <GeometryDrawing Brush=""#FFF6F6F6"" Geometry=""F1M0,14L16,14 16,1.999 0,1.999z"" />
              <GeometryDrawing Brush=""#FF414141"" Geometry=""F1M9,7L13,7 13,6 9,6z"" />
              <GeometryDrawing Brush=""#FF414141"" Geometry=""F1M9,9L13,9 13,8 9,8z"" />
              <GeometryDrawing Brush=""#FF414141"" Geometry=""F1M9,11L13,11 13,10 9,10z"" />
              <GeometryDrawing Brush=""#FF414141"" Geometry=""F1M14,12L2,12 2,5 14,5z M1,13L15,13 15,3 1,3z"" />
              <GeometryDrawing Brush=""#FF414141"" Geometry=""F1M3.8965,7.6035L4.6035,6.8965 5.5005,7.7925 6.3965,6.8965 7.1035,7.6035 6.2075,8.4995 7.1035,9.3965 6.3965,10.1035 5.5005,9.2075 4.6035,10.1035 3.8965,9.3965 4.7925,8.4995z M5.5005,10.9995C6.8805,10.9995 8.0005,9.8805 8.0005,8.4995 8.0005,7.1195 6.8805,5.9995 5.5005,5.9995 4.1195,5.9995 3.0005,7.1195 3.0005,8.4995 3.0005,9.8805 4.1195,10.9995 5.5005,10.9995"" />
              <GeometryDrawing Brush=""#FFF0EFF1"" Geometry=""F1M13,7L9,7 9,6 13,6z M13,9L9,9 9,8 13,8z M13,11L9,11 9,10 13,10z M5.5,11C4.119,11 3,9.881 3,8.5 3,7.119 4.119,6 5.5,6 6.881,6 8,7.119 8,8.5 8,9.881 6.881,11 5.5,11 M2,12L14,12 14,5 2,5z"" />
              <GeometryDrawing Brush=""#FFF0EFF1"" Geometry=""F1M6.3965,6.8965L5.5005,7.7925 4.6035,6.8965 3.8965,7.6035 4.7925,8.5005 3.8965,9.3965 4.6035,10.1035 5.5005,9.2075 6.3965,10.1035 7.1035,9.3965 6.2075,8.5005 7.1035,7.6035z"" />
            </DrawingGroup.Children>
          </DrawingGroup>
        </DrawingBrush.Drawing>
      </DrawingBrush>
    </Rectangle.Fill>
  </Rectangle>
</Viewbox>");
            XmlReader xmlReader = XmlReader.Create(stringReader);
            return XamlReader.Load(xmlReader) as Viewbox;
        }
    }
}