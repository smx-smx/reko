#region License
/* 
 * Copyright (C) 1999-2017 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using System;
using System.Windows.Forms;
using Reko.Core.Types;
using Reko.Core.Types.StringTypes;

namespace Reko.Gui.Windows.Forms
{
    public partial class FindStringsDialog : Form, IFindStringsDialog
    {
        public FindStringsDialog()
        {
            InitializeComponent();
            new FindStringsDialogInteractor().Attach(this);
        }

        public int MinLength { get {  return Convert.ToInt32(this.numericUpDown1.Value); } set { } }

        public ComboBox CharacterSizeList {  get { return ddlCharSize; } }

        public ComboBox StringKindList { get { return ddlStringKind; } }

        public StringType GetStringType ()
        {
            Type type;
            switch (ddlCharSize.SelectedIndex) {
                default:
                    type = typeof(AsciiStringType);
                    break;
                case 1:
                    type = typeof(UnicodeStringType);
                    break;
            }

            PrimitiveType prefixType = null;
            //$TODO: Should this be user selectable?
            int prefixOffset = 0;

            switch (ddlStringKind.SelectedIndex) {
                case 1:
                    prefixType = PrimitiveType.Byte;
                    break;
                case 2:
                    prefixType = PrimitiveType.UInt16;
                    break;
                case 3:
                    prefixType = PrimitiveType.UInt32;
                    break;
            }

            return Activator.CreateInstance(type, new object[]{
                prefixType, prefixOffset
            }) as StringType;
        }
    }
}
