#region License
/* 
* Copyright (C) 1999-2020 John Källén.
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

using Reko.Core;
using Reko.Gui.Controls;
using Reko.Gui.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reko.UserInterfaces.WindowsForms.Forms
{
    public partial class BlockNameDialog : Form, IBlockNameDialog
    {
        public BlockNameDialog(Procedure proc, Block block)
        {
            InitializeComponent();
            BlockId = new LabelWrapper(lblBlockID);
            BlockName = new TextBoxWrapper(txtUserName);
            OkButton = new ButtonWrapper(btnOK);
            this.ErrorMessage = new LabelWrapper(lblErrorText);
            new BlockNameInteractor(this, proc, block);
        }

        public ILabel BlockId { get; }

        public ITextBox BlockName { get; }

        public ILabel ErrorMessage { get; }

        public IButton OkButton { get; }
    }
}
