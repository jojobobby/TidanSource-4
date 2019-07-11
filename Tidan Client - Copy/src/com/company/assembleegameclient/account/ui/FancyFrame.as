
package com.company.assembleegameclient.account.ui {


import com.company.assembleegameclient.ui.DeprecatedClickableText;
import com.company.ui.BaseSimpleText;
import com.company.util.GraphicsUtil;

import flash.display.CapsStyle;
import flash.display.DisplayObject;
import flash.display.GraphicsPath;
import flash.display.GraphicsSolidFill;
import flash.display.GraphicsStroke;
import flash.display.IGraphicsData;
import flash.display.JointStyle;
import flash.display.LineScaleMode;
import flash.display.Sprite;
import flash.events.Event;
import flash.filters.DropShadowFilter;

import kabam.rotmg.pets.view.components.DialogCloseButton;

import mx.controls.TextInput;

import spark.components.CheckBox;

public class FancyFrame extends Sprite
{

    public function FancyFrame(_arg1:String, button1Text:String, _arg5:int = 288) {
        this.frameTextInputBoxes = new Vector.<TextInputField>();
        this.frameTextButtons_ = new Vector.<DeprecatedClickableText>();
        this.primaryColorLight = new GraphicsSolidFill(0x303030, 1);
        this.primaryColorDark = new GraphicsSolidFill(0x444444, 1);
        this.outlineFill_ = new GraphicsSolidFill(0xFFFFFF, 1);
        this._graphicsStroke = new GraphicsStroke(1, false, LineScaleMode.NORMAL, CapsStyle.NONE, JointStyle.ROUND, 3, this.outlineFill_);
        this.path1_ = new GraphicsPath(new Vector.<int>(), new Vector.<Number>());
        this.path2_ = new GraphicsPath(new Vector.<int>(), new Vector.<Number>());
        this.graphicsData_ = new <IGraphicsData>[primaryColorDark, path2_, GraphicsUtil.END_FILL, primaryColorLight, path1_, GraphicsUtil.END_FILL, _graphicsStroke, path2_, GraphicsUtil.END_STROKE];
        super();
        this.w_ = _arg5;
        this.frameTitle = new BaseSimpleText(12, 0xFFFFFF, false, 0, 0);
        this.frameTitle.text = _arg1;
        this.frameTitle.updateMetrics();
        this.frameTitle.setBold(true);
        this.frameTitle.filters = [new DropShadowFilter(0, 0, 0)];
        this.frameTitle.x = 90;
        this.frameTitle.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        addChild(this.frameTitle);
        this.Button1 = new DeprecatedClickableText(18, true, button1Text);
        if (button1Text != "") {
            this.Button1.buttonMode = true;
            this.Button1.x = (this.w_ / 2) - (Button1.width / 2);
            addChild(this.Button1);
        }
        this.XButton = new DialogCloseButton();
        this.XButton.x = ((this.w_ - this.XButton.width) - 7);
        this.XButton.y = ((this.h_ - this.XButton.width) - 284);
        addChild(this.XButton);
        filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        addEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
        addEventListener(Event.REMOVED_FROM_STAGE, this.onRemovedFromStage);
    }
    public var frameTitle:BaseSimpleText;
    public var Button1:DeprecatedClickableText;
    public var XButton:DialogCloseButton;
    public var frameTextInputBoxes:Vector.<TextInputField>;
    public var frameTextButtons_:Vector.<DeprecatedClickableText>;
    protected var w_:int = 288;
    protected var h_:int = 300;
    private var graphicsData_:Vector.<IGraphicsData>;
    private var primaryColorLight:GraphicsSolidFill;
    private var primaryColorDark:GraphicsSolidFill;
    private var outlineFill_:GraphicsSolidFill;
    private var _graphicsStroke:GraphicsStroke;
    private var path1_:GraphicsPath;
    private var path2_:GraphicsPath;


    public function draw():void {
        this.graphics.clear();
        GraphicsUtil.clearPath(this.path1_);
        GraphicsUtil.drawUI(-6, -6, this.w_, (20 + 12), 4, [1, 1, 0, 0], this.path1_);
        GraphicsUtil.clearPath(this.path2_);
        GraphicsUtil.drawUI(-6, -6, this.w_, this.h_, 4, [1, 1, 1, 1], this.path2_);
        (this.Button1.y = (this.h_ - 48));
        this.graphics.drawGraphicsData(this.graphicsData_);
    }

    public function onAddedToStage(_arg1:Event):void {
        this.draw();
        x = ((stage.stageWidth / 2) - ((this.w_ - 6) / 2));
        y = ((stage.stageHeight / 2) - (height / 2));
        if (this.frameTextInputBoxes.length > 0) {
            (stage.focus = this.frameTextInputBoxes[0].inputText_);
        }
    }

    private function onRemovedFromStage(_arg1:Event):void {
    }

}
}

