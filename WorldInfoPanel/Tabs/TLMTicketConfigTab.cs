﻿using Klyte.Commons.Utils;
using Klyte.TransportLinesManager.Extensions;
using Klyte.TransportLinesManager.Utils;
using Klyte.TransportLinesManager.Xml;
using System.Collections.Generic;
using UnityEngine;

namespace Klyte.TransportLinesManager.UI
{

    public class TLMTicketConfigTab : TLMBaseTimedConfigTab<TLMTicketConfigTab, TLMTicketPriceTimeChart, TLMTicketPriceEditorLine, TicketPriceEntryXml>
    {
        public override string GetTitleLocale() => "K45_TLM_PER_HOUR_TICKET_PRICE_TITLE";
        public override string GetValueColumnLocale() => "K45_TLM_TICKET_PRICE";

        public override void ExtraAwake() => m_uiHelper.AddSpace(20);

        public override float GetMaxSliderValue()
        {
            var tsd = TransportSystemDefinition.GetDefinitionForLine(ref TransportManager.instance.m_lines.m_buffer[UVMPublicTransportWorldInfoPanel.GetLineID()]);
            return TLMLineUtils.GetTicketPriceForLine(tsd, 0).First.Value * 5;
        }

        internal override List<Color> ColorOrder { get; } = new List<Color>()
        {
            Color.Lerp(Color.red,Color.magenta,0.5f),
            Color.magenta,
            Color.Lerp(Color.blue,Color.magenta,0.5f),
            Color.blue,
            Color.Lerp(Color.blue,Color.cyan,0.5f),
            Color.cyan,
            Color.green,
            Color.yellow,
            Color.Lerp(Color.red,Color.yellow,0.5f),
            Color.red,
        };

        protected override TimeableList<TicketPriceEntryXml> Config => TLMLineUtils.GetEffectiveConfigForLine(UVMPublicTransportWorldInfoPanel.GetLineID()).TicketPriceEntries;

        protected override TicketPriceEntryXml DefaultEntry() => new TicketPriceEntryXml()
        {
            HourOfDay = 0,
            Value = 0
        };
        public override string GetTemplateName() => TLMTicketPriceEditorLine.TICKET_PRICE_LINE_TEMPLATE;
        public override void EnsureTemplate() => TLMTicketPriceEditorLine.EnsureTemplate();
    }

}
