package kabam.rotmg.ui.defaults
{
import com.company.assembleegameclient.util.FilterUtil;
import flash.filters.DropShadowFilter;
import flash.text.TextFormat;
import flash.text.TextFormatAlign;
import kabam.rotmg.ui.labels.UILabel;
import kabam.rotmg.text.model.FontModel;

public class DefaultLabelFormat
{


    public function DefaultLabelFormat()
    {
        super();
    }

    public static function createLabelFormat(_arg_1:UILabel, _arg_2:int = 12, _arg_3:Number = 16777215, _arg_4:String = "left", _arg_5:Boolean = false, _arg_6:Array = null) : void
    {
        var _local_7:TextFormat = createTextFormat(_arg_2,_arg_3,_arg_4,_arg_5);
        applyTextFormat(_local_7,_arg_1);
        if(_arg_6)
        {
            _arg_1.filters = _arg_6;
        }
    }

    public static function defaultButtonLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,16,16777215,TextFormatAlign.CENTER);
    }

    public static function defaultPopupTitle(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,32,15395562,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function defaultSmallPopupTitle(_arg_1:UILabel, _arg_2:String = "left") : void
    {
        createLabelFormat(_arg_1,14,15395562,_arg_2,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function friendsItemLabel(_arg_1:UILabel, _arg_2:Number = 16777215) : void
    {
        createLabelFormat(_arg_1,14,_arg_2,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function guildInfoLabel(_arg_1:UILabel, _arg_2:int = 14, _arg_3:Number = 16777215, _arg_4:String = "left") : void
    {
        createLabelFormat(_arg_1,_arg_2,_arg_3,_arg_4,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function characterViewNameLabel(_arg_1:UILabel, _arg_2:int = 18) : void
    {
        createLabelFormat(_arg_1,_arg_2,11776947,TextFormatAlign.LEFT,true,[new DropShadowFilter(0,0,0)]);
    }

    public static function characterFameNameLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,16777215,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function characterFameInfoLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,9211020,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function characterToolTipLabel(_arg_1:UILabel, _arg_2:Number) : void
    {
        createLabelFormat(_arg_1,12,_arg_2,TextFormatAlign.LEFT,true);
    }

    public static function coinsFieldLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,16777215,TextFormatAlign.RIGHT);
    }

    public static function numberSpinnerLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,15395562,TextFormatAlign.CENTER);
    }

    public static function shopTag(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,16777215,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter02());
    }

    public static function popupTag(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,24,16777215,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter02());
    }

    public static function shopBoxTitle(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,15395562,TextFormatAlign.LEFT);
    }

    public static function defaultModalTitle(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,16777215,TextFormatAlign.LEFT,false,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function defaultTextModalText(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,16777215,TextFormatAlign.CENTER);
    }

    public static function mysteryBoxContentInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,10066329,TextFormatAlign.CENTER,true);
    }

    public static function mysteryBoxContentItemName(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,16777215,TextFormatAlign.LEFT);
    }

    public static function popupEndsIn(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,24,16684800,TextFormatAlign.LEFT,true,FilterUtil.getUILabelComboFilter());
    }

    public static function mysteryBoxEndsIn(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,16684800,TextFormatAlign.LEFT,true,FilterUtil.getUILabelComboFilter());
    }

    public static function popupStartsIn(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,24,16728576,TextFormatAlign.LEFT,true,FilterUtil.getUILabelComboFilter());
    }

    public static function mysteryBoxStartsIn(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,16728576,TextFormatAlign.LEFT,true,FilterUtil.getUILabelComboFilter());
    }

    public static function priceButtonLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,15395562,TextFormatAlign.LEFT,false,FilterUtil.getUILabelDropShadowFilter01());
    }

    public static function originalPriceButtonLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,16,15395562,TextFormatAlign.LEFT,false,FilterUtil.getUILabelComboFilter());
    }

    public static function defaultInactiveTab(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,6381921,TextFormatAlign.LEFT,true);
    }

    public static function defaultActiveTab(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,15395562,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter02());
    }

    public static function mysteryBoxVaultInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,16684800,TextFormatAlign.LEFT,true,FilterUtil.getUILabelDropShadowFilter02());
    }

    public static function currentFameLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,22,16760388,TextFormatAlign.LEFT,true);
    }

    public static function deathFameLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,15395562,TextFormatAlign.LEFT,true);
    }

    public static function deathFameCount(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,16762880,TextFormatAlign.RIGHT,true);
    }

    public static function tierLevelLabel(_arg_1:UILabel, _arg_2:int = 12, _arg_3:Number = 16777215, _arg_4:String = "right") : void
    {
        createLabelFormat(_arg_1,_arg_2,_arg_3,_arg_4,true);
    }

    public static function questRefreshLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,10724259,TextFormatAlign.CENTER,true);
    }

    public static function questCompletedLabel(_arg_1:UILabel, _arg_2:Boolean, _arg_3:Boolean) : void
    {
        var _local_4:Number = !!_arg_2?Number(3971635):Number(13224136);
        createLabelFormat(_arg_1,16,_local_4,TextFormatAlign.LEFT,true);
    }

    public static function questNameLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,20,15241232,TextFormatAlign.CENTER,true);
    }

    public static function notificationLabel(_arg_1:UILabel, _arg_2:int, _arg_3:Number, _arg_4:String, _arg_5:Boolean) : void
    {
        createLabelFormat(_arg_1,_arg_2,_arg_3,_arg_4,_arg_5,FilterUtil.getUILabelDropShadowFilter01());
    }

    private static function applyTextFormat(_arg_1:TextFormat, _arg_2:UILabel) : void
    {
        _arg_2.defaultTextFormat = _arg_1;
        _arg_2.setTextFormat(_arg_1);
    }

    public static function createTextFormat(_arg_1:int, _arg_2:uint, _arg_3:String, _arg_4:Boolean) : TextFormat
    {
        var _local_5:TextFormat = new TextFormat();
        _local_5.color = _arg_2;
        _local_5.bold = _arg_4;
        _local_5.font = FontModel.DEFAULT_FONT_NAME;
        _local_5.size = _arg_1;
        _local_5.align = _arg_3;
        return _local_5;
    }

    public static function questDescriptionLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,16,13224136,TextFormatAlign.CENTER);
    }

    public static function questRewardLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,16,16777215,TextFormatAlign.CENTER,true);
    }

    public static function questChoiceLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,13224136,TextFormatAlign.CENTER);
    }

    public static function questButtonCompleteLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,16777215,TextFormatAlign.CENTER,true);
    }

    public static function questNameListLabel(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,14,_arg_2,TextFormatAlign.LEFT,true);
    }

    public static function petNameLabel(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,18,_arg_2,TextFormatAlign.CENTER,true);
    }

    public static function petNameLabelSmall(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,14,_arg_2,TextFormatAlign.CENTER,true);
    }

    public static function petFamilyLabel(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,14,_arg_2,TextFormatAlign.CENTER,true,FilterUtil.getUILabelComboFilter());
    }

    public static function petInfoLabel(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,12,_arg_2,TextFormatAlign.CENTER);
    }

    public static function petStatLabelLeft(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,12,_arg_2,TextFormatAlign.LEFT);
    }

    public static function petStatLabelRight(_arg_1:UILabel, _arg_2:uint, _arg_3:Boolean = false) : void
    {
        createLabelFormat(_arg_1,12,_arg_2,TextFormatAlign.RIGHT,_arg_3);
    }

    public static function petStatLabelLeftSmall(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,11,_arg_2,TextFormatAlign.LEFT);
    }

    public static function petStatLabelRightSmall(_arg_1:UILabel, _arg_2:uint, _arg_3:Boolean = false) : void
    {
        createLabelFormat(_arg_1,11,_arg_2,TextFormatAlign.RIGHT,_arg_3);
    }

    public static function fusionStrengthLabel(_arg_1:UILabel, _arg_2:uint, _arg_3:int) : void
    {
        var _local_4:Number = _arg_3 != 0?Number(_arg_2):Number(16777215);
        createLabelFormat(_arg_1,14,_local_4,TextFormatAlign.CENTER,true);
    }

    public static function feedPetInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,16777215,TextFormatAlign.CENTER,true);
    }

    public static function wardrobeCollectionLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,16777215,TextFormatAlign.CENTER,true);
    }

    public static function petYardRarity(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,10658466,TextFormatAlign.CENTER,true);
    }

    public static function petYardUpgradeInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,9211020,TextFormatAlign.CENTER,true);
    }

    public static function petYardUpgradeRarityInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,16777215,TextFormatAlign.CENTER,true);
    }

    public static function newAbilityInfo(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,12,10066329,TextFormatAlign.CENTER,true);
    }

    public static function newAbilityName(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,8971493,TextFormatAlign.CENTER,true);
    }

    public static function newSkinHatched(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,14,9671571,TextFormatAlign.CENTER,true);
    }

    public static function infoTooltipText(_arg_1:UILabel, _arg_2:uint) : void
    {
        createLabelFormat(_arg_1,14,_arg_2,TextFormatAlign.LEFT);
    }

    public static function newSkinLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,9,0,TextFormatAlign.CENTER,true);
    }

    public static function donateAmountLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,15395562,TextFormatAlign.RIGHT,false);
    }

    public static function pointsAmountLabel(_arg_1:UILabel) : void
    {
        createLabelFormat(_arg_1,18,15395562,TextFormatAlign.CENTER,false);
    }
}
}
