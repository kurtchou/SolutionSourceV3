"use strict";

//declare all global variables here, name should start with g
var gPhoneViewFlag = 0;//0 is not phone view, 1 is phone view

function ReplaceLoopUpMultiID(strArray) {
    var arrValue = []
    var arrLookUp = strArray.split(";#");
    var returnStr = "";
    for (var i = 0; i < arrLookUp.length; i++) {
        if (!isNaN(arrLookUp[i]) == false) {
            returnStr = returnStr + arrLookUp[i] + ", "
        }
    }
    return returnStr.substring(0, returnStr.length - 2);
}

function ExpandAllAsset() {
    expandcontract.sweepToggle("expand");
}

function ContractAllAsset() {
    expandcontract.sweepToggle("contract");
}

function Base64EncodeString(UnencodedString) {
    //var encodedString = btoa(encodeURIComponent(UnencodedString));
    var encodedString = btoa(UnencodedString);
    return encodedString;
}

function Base64DecodeString(EncodedString) {
    //decodedString = atob(decodeURIComponent(EncodedString));
    decodedString = atob(EncodedString);
    return decodedString;
}
