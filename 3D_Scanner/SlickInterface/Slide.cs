/*

  SlickInterface
  Copyright (c) 2010, Christoph Heindl
  All rights reserved.

  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  - Redistributions of source code must retain the above copyright notice, this list 
    of conditions and the following disclaimer.
  - Redistributions in binary form must reproduce the above copyright notice, this list 
    of conditions and the following disclaimer in the documentation and/or other materials 
    provided with the distribution.
  - Neither the name of the Christoph Heindl nor the names of its contributors may be 
    used to endorse or promote products derived from this software without specific prior 
    written permission.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
  OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING 
  IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SlickInterface {

  /// <summary>
  /// Base class for slides.
  /// </summary>
  public partial class Slide : UserControl {
    private SlideControl _control;

    /// <summary>
    /// Initialize slide.
    /// </summary>
    public Slide() {
      InitializeComponent();
    }

    /// <summary>
    /// Get and set the slide control.
    /// </summary>
    [Browsable(false)]
    public SlideControl SlideControl {
      get { return _control; }
      set { _control = value; }
    }

    /// <summary>
    /// Whenever this slide is going to appear as selected slide at the parent SlideControl.
    /// </summary>
    protected virtual void OnSlidingIn(SlidingEventArgs e) { }

    /// <summary>
    /// Whenever this slide is about to clear the way for a new slide.
    /// </summary>
    /// <param name="e">OnSlidingOut arguments.</param>
    protected virtual void OnSlidingOut(SlidingEventArgs e) { }

    /// <summary>
    /// True when slide can occur, false otherwise
    /// </summary>
    /// <remarks>Defaukts to true.</remarks>
    /// <returns>True when slide can occur, false otherwise</returns>
    protected virtual bool SlideOutRequested() { return true; }

    /// <summary>
    /// True when slide can occur, false otherwise
    /// </summary>
    /// <remarks>Defaukts to true.</remarks>
    /// <returns>True when slide can occur, false otherwise</returns>
    protected virtual bool SlideInRequested() { return true; }

    /// <summary>
    /// Called from SlideControl
    /// </summary>
    internal void OnSlidingInInternal(SlidingEventArgs e) { this.OnSlidingIn(e); }
    /// <summary>
    /// Called from SlideControl
    /// </summary>
    /// <param name="e"></param>
    internal void OnSlidingOutInternal(SlidingEventArgs e) { this.OnSlidingOut(e); }
    /// <summary>
    /// Called from SlideControl
    /// </summary>
    /// <returns>True if slide out is allowed</returns>
    internal bool SlideOutRequestedInternal() { return this.SlideOutRequested(); }
    /// <summary>
    /// Called from SlideControl
    /// </summary>
    /// <returns>True if slide in is allowed</returns>
    internal bool SlideInRequestedInternal() { return this.SlideInRequested(); }
  }
}
