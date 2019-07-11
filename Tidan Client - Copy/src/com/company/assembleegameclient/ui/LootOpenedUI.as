package com.company.assembleegameclient.ui {
import com.company.assembleegameclient.account.ui.FancyFrame;
import com.company.assembleegameclient.game.AGameSprite;
import com.company.assembleegameclient.objects.ObjectLibrary;
import com.gskinner.motion.GTween;

import flash.display.Bitmap;
import flash.display.BitmapData;
import flash.display.Sprite;
import flash.events.MouseEvent;
import flash.events.TimerEvent;
import flash.utils.Timer;

import kabam.rotmg.messaging.impl.incoming.Text;

import kabam.rotmg.text.view.TextFieldDisplayConcrete;
import kabam.rotmg.text.view.stringBuilder.StaticStringBuilder;

import org.osflash.signals.Signal;


public class LootOpenedUI extends FancyFrame {

    private var itemBitmap:Bitmap;
    private var rewardText:TextFieldDisplayConcrete;
    private var congratText:TextFieldDisplayConcrete;
    public const cancel:Signal = new Signal();


    public var gameSprite:AGameSprite;
    public var Button:DeprecatedTextButton
    public function LootOpenedUI() {
        super("Loot Box Unlock UI", "", 288);
        DrawBackGround();
        makeRewardText();
        createItemBitmap();
        this.Button = new DeprecatedTextButton(16, "Close");
        this.Button.y = 250;
        this.Button.x = 108;
        addChild(this.Button);
        XButton.visible = false;
        Button.addEventListener(MouseEvent.CLICK, this.onClose);
    }


    private function DrawBackGround():void {
        this.graphics.beginFill(0x363636);
        this.graphics.lineStyle(1, 0xFFFFFF);
        this.graphics.drawRect(0, 0, 140, 100);
        this.graphics.endFill();
    }

    private function makeRewardText():void {
        this.rewardText = new TextFieldDisplayConcrete().setSize(12).setBold(false).setColor(0xFFFFFF);
        this.congratText = new TextFieldDisplayConcrete().setSize(12).setBold(true).setColor(0xFFFFFF);

        this.congratText.x = 10;
        this.congratText.y = 63;
        this.rewardText.x = 10;
        this.rewardText.y = 83;
        addChild(this.congratText);
        addChild(this.rewardText);
    }

    private function onClose(_arg1:MouseEvent):void {
        this.parent.removeChild(this);
    }

    private function createItemBitmap():void {
        this.itemBitmap = new Bitmap();
        this.itemBitmap.x = 0;
        this.itemBitmap.y = 0;
        addChild(this.itemBitmap);
    }

    public function setItemBitmap(_arg1:uint):void {
        this.itemBitmap.bitmapData = ObjectLibrary.getRedrawnTextureFromType(_arg1, 60, true);
        this.itemBitmap.scaleX = 1.7;
        this.itemBitmap.scaleY = 1.7;
        this.itemBitmap.x = this.width/2 - this.itemBitmap.width/2;
        this.itemBitmap.y = this.height/2 - this.itemBitmap.height/2;

        var _local1:String = ObjectLibrary.getIdFromType(_arg1);
        this.congratText.setStringBuilder(new StaticStringBuilder("Congratulations! You have un-boxed a:"))
        this.rewardText.setStringBuilder(new StaticStringBuilder(_local1));
    }
}
}
