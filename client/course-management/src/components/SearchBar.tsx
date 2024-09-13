'use client';

import { FC, useState } from 'react';

interface SearchBarProps {
  onSearch: (searchTerm: string) => void;
}

const SearchBar: FC<SearchBarProps> = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = () => {
    onSearch(searchTerm);
  };

  return (
    <div className="flex justify-center mb-4">
      <div className="flex items-center max-w-xl w-full">
        <input
          type="text"
          placeholder="Search by description ..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="border border-gray-300 rounded-md px-4 py-2 w-full mr-4"
        />
        <button
          onClick={handleSearch}
          className="bg-blue text-white px-4 py-2 rounded-md"
        >
          Search
        </button>
      </div>
    </div>
  );
};

export default SearchBar;
