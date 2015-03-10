using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network
{
    public enum MessageOp : uint
    {
        Null = 0x0,

        ClusterRegisterServer = 0xb88f5166u,
        ClusterClientConnect = 0x6B9E5323u,
        ClusterClientDisconnect = 0x44e7e4fau,
        ClusterZoneTransferRequestByTicket = 0x12BEB7E8u,
        ClusterZoneTransferRequestByPosition = 0xE259B191u,
        ClusterZoneTransferApprovedByPosition = 0xF5E12559u,
        ClusterZoneTransferApprovedByTicket = 0xA608F0B2u,
        ClusterZoneTransferDenied = 0x7B4AF214u,
        ClusterZoneTransferCharacter = 0x74C4FC34u,
        TutorialServerStatusRequest = 0x5E48A399u,
        TutorialServerStatusReply = 0x989EDF5Au,
        SelectCharacter = 0xb5098d76u,
        ConGenericMessage = 0x08C5FC76u,
        ErrorMessage = 0xb5abf91au,
        ClientIdMsg = 0xd5899226u,
        ClientPermissionsMessage = 0xE00730E5u,
        HeartBeat = 0xa16cf9afu,
        LoginClientToken = 0xAAB296C6u,
        LoginEnumCluster = 0xC11C63B9u,
        LoginClusterStatus = 0x3436AEB6u,
        EnumerateCharacterId = 0x65EA4574u,
        LoginClientId = 0x41131f96u,
        DeleteCharacterMessage = 0xe87ad031u,
        DeleteCharacterReplyMessage = 0x8268989bu,
        LauncherSessionen = 0x486f6f6eu,
        LauncherSessionCreated = 0x2e4e6574u,
        ClientInactivity = 0x0F5D5325u,
        ClientLogout = 0x42FD19DDu,

        //Harvester / House placement
        StructurePlacementMode = 0xe8a54dc1u,
        SendPermissionList = 0x52F364B8u,
        OperateHarvester = 0xBD18C679u,

        ClusterZoneRegisterName = 0xb88f5166u,
        NewbieTutorialResponse = 0xca88fbadu,
        NewbieTutorialRequest = 0x90DD61AFu,
        OpenHolocronToPageMessage = 0x7CB65021u,
        ChatServerStatus = 0x7102B15Fu,
        ParametersMessage = 0x487652DAu,
        CmdStartScene = 0x3AE6DFAEu,
        CmdSceneReady = 0x43FD1C22u,
        CmdSceneReady2 = 0x48f493c5u,
        ServerTimeMessage = 0x2EBC3BD9u,
        ServerWeatherMessage = 0x486356eau,
        ObjectMenuSelection = 0x7ca18726u,
        EnterTicketPurchaseModeMessage = 0x904dae1au,
        PlanetTravelPointListRequest = 0x96405d4du,
        PlanetTravelPointListResponse = 0x4d32541fu,
        ChatSystemMessage = 0x6d2a6413u,
        AttributeListMessage = 0xf3f12f2au,
        PlayClientEffectObjectMessage = 0x8855434au,
        PlayClientEffectLocMessage = 0x02949e74u,
        PlayMusicMessage = 0x04270d8au,
        ResourceListForSurveyMessage = 0x8a64b1d5u,
        SurveyMessage = 0x877f79acu,
        BadgesResponseMessage = 0x6d89d25bu,
        FactionRequestMessage = 0xc1b03b81u,
        FactionResponseMessage = 0x5dd53957u,
        StomachRequestMessage = 0xB75DD5D7u,
        StomachResponseMessage = 0x4a093f02u,
        PlayerMoneyRequest = 0x9D105AA1u,
        PlayerMoneyResponse = 0x367E737Eu,
        GuildRequestMessage = 0x81EB4EF7u,
        GuildResponseMessage = 0x32263F20u,
        CharacterSheetResponseMessage = 0x9B3A17C4u,

        //Trade
        BankTipDustOff = 0x4516EDA7u,
        GiveMoneyMessage = 0xD1527EE8u,
        UnacceptTransactionMessage = 0xE81E4382u,
        BeginVerificationMessage = 0xE7481DF5u,
        AcceptTransactionMessage = 0xB131CA17u,
        RemoveItemMessage = 0x4417AF8Bu,
        AddItemMessage = 0x1E8D1356u,
        TradeCompleteMessage = 0xC542038Bu,
        AbortTradeMessage = 0x9CA80F98u,
        BeginTradeMessage = 0x325932D8u,
        VerifyTradeMessage = 0x9AE247EEu,
        IsVendorMessage = 0x21B55A3Bu,
        IsVendorOwnerResponseMessage = 0xCE04173Eu,
        AuctionQueryHeadersMessage = 0x679E0D00u,
        AuctionQueryHeadersResponseMessage = 0xFA500E52u,
        CreateAuctionMessage = 0xAD47021Du,
        CreateImmediateAuctionMessage = 0x1E9CE308u,
        ProcessSendCreateItem = 0x4E5468E8u,
        DeductMoneyMessage = 0x10000000u,
        CreateAuctionMessageResponseMessage = 0xE61CC92u,
        GetAuctionDetails = 0xD36EFAE4u,
        GetAuctionDetailsResponse = 0xFE0E644Bu,
        CanceLiveAuctionMessage = 0x3687A4D2u,
        CanceLiveAuctionResponseMessage = 0x7DA2246Cu,
        RetrieveAuctionItemMessage = 0x12B0D449u,
        RetrieveAuctionItemResponseMessage = 0x9499EF8Cu,
        BidAuctionResponseMessage = 0x8FCBEF4Au,
        ProcessCreateAuction = 0xAC2FD41Du,

        LogoutMessage = 0x42FD19DDu,

        ObjControllerMessage = 0x80ce5e46u,

        SceneCreateObjectByCrc = 0xFE89DDEAu,
        UpdateContainmentMessage = 0x56CBDE9Eu,
        UpdatePostureMessage = 0x0BDE6B41u,
        UpdateCellPermissionMessage = 0xf612499cu,
        BaselinesMessage = 0x68A75F0Cu,
        DeltasMessage = 0x12862153u,
        SceneEndBaselines = 0x2C436037u,
        SceneDestroyObject = 0x4D45D504u,
        UpdateTransformMessage = 0x1B24F808u,
        UpdateTransformMessageWithParent = 0xC867AB5Au,
        UpdatePvpStatusMessage = 0x08a1c126u,
        ChangePosture = 0xab290245u,
        OpenedContainer = 0x2E11E4ABu,
        ClosedContainer = 0x32B79B7Eu, //not functional serverpacket to client

        //social
        FindFriendCreateWaypoint = 0xDDA2B297u,
        FindFriendRequestPosition = 0x35D536D9u,
        FindFriendSendPosition = 0x7347C6BFu,
        NotifyChatAddFriend = 0xb581f90du,
        NotifyChatRemoveFriend = 0x336015cu,
        NotifyChatFindFriend = 0xc447e379u,
        NotifyChatAddIgnore = 0xd387da5u,
        NotifyChatRemoveIgnore = 0xbb8f85f4u,

        GRUP = 0x47525550u,
        CREO = 0x4352454Fu,
        PLAY = 0x504c4159u,
        TANO = 0x54414e4fu,
        FCYT = 0x46435954u,

        BUIO = 0x4255494fu,
        HINO = 0x48494E4Fu,
        INSO = 0x494E534Fu,
        SCLT = 0x53434c54u,
        RCNO = 0x52434e4fu,
        MSCO = 0x4d53434fu,
        MISO = 0x4d49534fu,
        ITNO = 0x49544e4fu,
        SecureTrade = 0x00000115u,
        STAO = 0x5354414Fu,
        //ImageDesigner
        StatMigrationStart = 0xefac38c4u,

        // group ISM (inter-server-messages)

        IsmSendSystemMailMessage = 0x7B08578Eu,
        IsmGroupInviteRequest = 0x944F2822u, //[Both]	<uint32 target>
        IsmGroupInviteResponse = 0x3FDF93DFu, //[Both]	<bool success>
        IsmGroupUnInvite = 0x384CBE2Cu, //[Both]	<uint32 target>
        IsmGroupCREO6deltaGroupId = 0xF923A570u, //[CH->ZO]	<uint32 target><uint64 groupId>
        IsmGroupDisband = 0xEFF47552u, //[ZO->CH]
        IsmGroupLeave = 0x2629856Bu, //[ZO->CH]
        IsmGroupLootModeRequest = 0xAD02C6DAu, //[ZO->CH]	<>
        IsmGroupLootModeResponse = 0xF0D065EEu, //[Both]
        IsmGroupLootMasterRequest = 0xE91B2BBDu, //[ZO->CH]	<>
        IsmGroupLootMasterResponse = 0xCEBF0445u, //[Both]
        IsmGroupDismissGroupMember = 0x49BA6C4Cu, //[ZO->CH]	<uint32 target>
        IsmGroupMakeLeader = 0x9338ADA4u, //[ZO->CH]	<uint32 target>
        IsmGroupPositionNotification = 0x2FB45F7Bu, //[ZO->CH]	<float x><float z>
        IsmGroupBaselineRequest = 0xB0CE545Au, //[ZO->CH]	<float x><float z>
        IsmGroupAlterMissions = 0x2F46E3A3u, //[ZO->CH]
        IsmGroupInviteInRangeRequest = 0x19F89B8Eu, //[CH->ZO]  <uint32 sender><uint32 target>
        IsmGroupInviteInRangeResponse = 0xA4B7CA4Cu, //[ZO->CH]  <uint32 sender><uint32 target><bool inRange>
        IsmIsGroupLeaderRequest = 0xC85225C9u,
        IsmIsGroupLeaderResponse = 0x1B53DB12u,

        // Fake code, crc-based an the word "opIsmGroupSay".
        IsmGroupSay = 0x39360616u, //[ZO->CH]  // Fake code, crc-based on the word "IsmGroupSay".
        ChatNotifySceneReady = 0x75C4DD84u, //[ZO ->CH] // Fake code, crc-based on the word "chatcmdsceneready".
        BankTipDeduct = 0x723BF836u,

        // admin inter server messages
        IsmBroadcastPlanet = 0x3F9D6D6Eu, //[ZO->CH]
        IsmBroadcastGalaxy = 0x8E41B5CBu, //[ZO->CH]
        IsmScheduleShutdown = 0xF2477D2Cu, //[ZO->CH] [CH->ZO]
        IsmCancelShutdown = 0x5E43AC09u, //[ZO->CH]

        // structure inter server messages
        IsmHarvesterUpdate = 0x8F603896u, //[ZO->CH]

        CancelLiveAuctionMessage = 0x3687A4D2u,
        BidAuctionMessage = 0x91125453u,
        GetCommoditiesTypeList = 0x48F493C5u,
        CommoditiesTypeListResponse = 0xD4E937FCu,

        //inter Server Kommunikation
        BidAuctionAcceptedMessage = 0x8737A639u,
        SendSystemMailMessage = 0x7B08578Eu,

        ConnectPlayerMessage = 0x2e365218u,
        ConnectPlayerResponseMessage = 0x6137556Fu,

        ChatRequestRoomlist = 0x4c3d2cfau,
        ChatRoomlist = 0x70deb197u,

        ChatCreateRoom = 0x35366bedu,
        ChatOnCreateRoom = 0x35d7cc9fu,

        ChatDestroyRoom = 0x094b2a77u,
        ChatOnDestroyRoom = 0xe8ec5877u,

        ChatEnterRoomById = 0xbc6bddf2u,
        ChatOnEnteredRoom = 0xe69bdc0au,

        ChatOnLeaveRoom = 0x60b5098bu,

        ChatQueryRoom = 0x9cf2b192u,
        ChatQueryRoomResults = 0xc4de864eu,

        ChatSendToRoom = 0x20e4dbe3u,
        ChatOnSendRoomMessage = 0xe7b61633u,
        ChatRoomMessage = 0xcd4ce444u,

        ChatAddModeratorToRoom = 0x90bde76fu,
        ChatOnModerateRoom = 0x36a03858u,

        ChatInviteAvatarToRoom = 0x7273ecd3u,
        ChatOnInviteRoom = 0x493fe74au,

        ChatOnReceiveRoomInvite = 0xc17eb06du,

        ChatRemoveModFromRoom = 0x8a3f8e04u,
        ChatOnRemoveModFromRoom = 0x1342fc47u,

        ChatRemoveAvatarFromRoom = 0x493e3ffau,

        ChatUninviteFromRoom = 0xfc8d01f1u,
        ChatOnUninviteFromRoom = 0xbe33c7e8u,

        ChatBanAvatarFromRoom = 0xd9fa0194u,
        ChatOnBanAvatarFromRoom = 0x5a38538du,

        ChatUnbanAvatarFromRoom = 0x4c8f94a9u,
        ChatOnUnbanAvatarFromRoom = 0xbaf9b815u,

        ChatAvatarId = 0x179a47feu,

        ChatOnConnectAvatar = 0xD72FE9BEu,

        // instant messages
        ChatInstantMessageToCharacter = 0x84bb21f7u, // C -> S
        ChatInstantMessageToClient = 0x3c565cedu, // S -> C(target)
        ChatOnSendInstantMessage = 0x88dbb381u, // S -> C(sender)

        // mail
        ChatPersistentMessageToServer = 0x25a29fa6u, // C -> S
        ChatPersistentMessageToClient = 0x08485e17u, // S -> C (target)
        ChatOnSendPersistentMessage = 0x94e7a7aeu, // S -> C (sender)

        ChatRequestPersistentMessage = 0x07e3559fu,

        ChatDeletePersistentMessage = 0x8f251641u,
        ChatOnDeletePersistentMessage = 0x4f23965au,

        // friendslist

        // ignoreList

        // This one is not verified or explained in wiki (afaik)
        // We get it when we log in after a Ctrl+escape (Disconnect).
        // The ..Play9 handles the update so far, but we don't wanna see all these un-handled messages.
        // TODO: Implement
        GetIgnorelist = 0x788BA6A3u,

        // note: addfriend, addignore are sent through ObjControllerMessage
        // didnt see those in precu yet...
        ChatFriendlistUpdate = 0x6cd2fcd8u,
        ChatAddFriend = 0x6c002d13u,

        ChatFriendList = 0xe97ab594u,

        // This one is not verified or explained in wiki (afaik)
        ChatIgnoreList = 0xea566326u,

        ClientMfdStatusUpdateMessage = 0x2D2D6EE1u,

        ClientCreateCharacter = 0xB97F3074u,
        ClientRandomNameRequest = 0xD6D1B6D1u,
        ClientRandomNameResponse = 0xE85FB868u,
        ClientCreateCharacterSuccess = 0x1DB575CCu,
        ClientCreateCharacterFailed = 0xdf333c6eu,
        LagRequest = 0x31805EE0u,

        // Map location messages
        GetMapLocationsMessage = 0x1a7ab839u,
        GetMapLocationsResponseMessage = 0x9f80464cu,
    }
}
