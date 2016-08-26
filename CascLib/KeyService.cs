﻿using System.Collections.Generic;

namespace CASCExplorer
{
    class KeyService
    {
        private static Dictionary<ulong, byte[]> keys = new Dictionary<ulong, byte[]>()
        {
            // hardcoded Overwatch keys
            [0x402CD9D8D6BFED98] = "AEB0EADEA47612FE6C041A03958DF241".ToByteArray(),
            [0xFB680CB6A8BF81F3] = "62D90EFA7F36D71C398AE2F1FE37BDB9".ToByteArray(),
            // streamed Overwatch keys
            [0xDBD3371554F60306] = "34E397ACE6DD30EEFDC98A2AB093CD3C".ToByteArray(),
            [0x11A9203C9881710A] = "2E2CB8C397C2F24ED0B5E452F18DC267".ToByteArray(),
            [0xA19C4F859F6EFA54] = "0196CB6F5ECBAD7CB5283891B9712B4B".ToByteArray(),
            [0x87AEBBC9C4E6B601] = "685E86C6063DFDA6C9E85298076B3D42".ToByteArray(),
            [0xDEE3A0521EFF6F03] = "AD740CE3FFFF9231468126985708E1B9".ToByteArray(),
            [0x8C9106108AA84F07] = "53D859DDA2635A38DC32E72B11B32F29".ToByteArray(),
            [0x49166D358A34D815] = "667868CD94EA0135B9B16C93B1124ABA".ToByteArray(),
            [0x1463A87356778D14] = "69BD2A78D05C503E93994959B30E5AEC".ToByteArray(),
            [0x5E152DE44DFBEE01] = "E45A1793B37EE31A8EB85CEE0EEE1B68".ToByteArray(),
            [0x9B1F39EE592CA415] = "54A99F081CAD0D08F7E336F4368E894C".ToByteArray(),
            [0x24C8B75890AD5917] = "31100C00FDE0CE18BBB33F3AC15B309F".ToByteArray(),
            [0xEA658B75FDD4890F] = "DEC7A4E721F425D133039895C36036F8".ToByteArray(),
            [0x026FDCDF8C5C7105] = "8F41809DA55366AD416D3C337459EEE3".ToByteArray(),
            [0xCAE3FAC925F20402] = "98B78E8774BF275093CB1B5FC714511B".ToByteArray(),
            [0x57A5A33B226B8E0A] = "FDFC35C99B9DB11A326260CA246ACB41".ToByteArray(),
            [0x4F0FE18E9FA1AC1A] = "89381C748F6531BBFCD97753D06CC3CD".ToByteArray(),
            [0x7758B2CF1E4E3E1B] = "3DE60D37C664723595F27C5CDBF08BFA".ToByteArray(),
            [0x42B9AB1AF5015920] = "C68778823C964C6F247ACC0F4A2584F8".ToByteArray(),
            // streamed WoW keys
            [0xFA505078126ACB3E] = "BDC51862ABED79B2DE48C8E7E66C6200".ToByteArray(), // TactKeyId 15
            [0xFF813F7D062AC0BC] = "AA0B5C77F088CCC2D39049BD267F066D".ToByteArray(), // TactKeyId 25
            [0xD1E9B5EDF9283668] = "8E4A2579894E38B4AB9058BA5C7328EE".ToByteArray(), // TactKeyId 39
            [0xB76729641141CB34] = "9849D1AA7B1FD09819C5C66283A326EC".ToByteArray(), // TactKeyId 40
            [0xFFB9469FF16E6BF8] = "D514BD1909A9E5DC8703F4B8BB1DFD9A".ToByteArray(), // TactKeyId 41
            [0x23C5B5DF837A226C] = "1406E2D873B6FC99217A180881DA8D62".ToByteArray(), // TactKeyId 42
            [0xE2854509C471C554] = "433265F0CDEB2F4E65C0EE7008714D9E".ToByteArray(), // TactKeyId 52
            [0xC4A8D364D23793F7] = "D1AC20FD14957FABC27196E9F6E7024A".ToByteArray(), // TactKeyId 58
            [0x40A234AEBCF2C6E5] = "C6C5F6C7F735D7D94C87267FA4994D45".ToByteArray(), // TactKeyId 59
            [0x94A50AC54EFF70E4] = "C2501A72654B96F86350C5A927962F7A".ToByteArray(), // TactKeyId 62
            [0x0B5F6957915ADDCA] = "4DD0DC82B101C80ABAC0A4D57E67F859".ToByteArray(), // TactKeyId 66
            [0x5E5D896B3E163DEA] = "8ACE8DB169E2F98AC36AD52C088E77C1".ToByteArray(), // TactKeyId 69
            [0x0EBE36B5010DFD7F] = "9A89CC7E3ACB29CF14C60BC13B1E4616".ToByteArray(), // TactKeyId 70
            [0x01E828CFFA450C0F] = "972B6E74420EC519E6F9D97D594AA37C".ToByteArray(), // TactKeyId 71
            // BNA 1.5.0 Alpha
            [0x2C547F26A2613E01] = "37C50C102D4C9E3A5AC069F072B1417D".ToByteArray(),
        };

        private static Salsa20 salsa = new Salsa20();

        public static Salsa20 SalsaInstance
        {
            get { return salsa; }
        }

        public static byte[] GetKey(ulong keyName)
        {
            byte[] key;
            keys.TryGetValue(keyName, out key);
            return key;
        }
    }
}
