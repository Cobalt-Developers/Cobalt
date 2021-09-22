using System;
using System.Collections.Generic;
using System.Linq;
using Cobalt.Api.Model;

namespace Cobalt.Api.Message
{
    public class PageableList
    {
        private string _title;
        private int _itemsPerPage;

        private List<string> _items;
        
        private int PageCount => _items.Count/_itemsPerPage + (_items.Count % _itemsPerPage > 0 ? 1 : 0);

        public PageableList(string title, List<string> items, int itemsPerPage = 10)
        {
            _title = title;
            _itemsPerPage = itemsPerPage;
            _items = items;
        }

        public void Print(IChatSender sender, int page = 1)
        {
            if(!IsValidPageNumber(page))
            {
                sender.SendErrorMessage($"The page-index needs to be between 1 an {PageCount}.");
                return;
            }
            
            sender.SendMessage(GetHeader());
            foreach (var line in GetItemsForPage(page)) sender.SendMessage(line);
            sender.SendMessage(GetFooter(page));
        }

        private bool IsValidPageNumber(int input)
        {
            return input >= 1 && input <= PageCount;
        }

        private string GetHeader()
        {
            return $"=====[ {_title.First().ToString().ToUpper()+_title.Substring(1).ToLower()} ]=====";
        }

        private string GetFooter(int page)
        {
            return $"=====[ {page} / {PageCount} ]=====";
        }

        private List<string> GetItemsForPage(int page)
        {
            return _items.Skip((page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        }
    }
}