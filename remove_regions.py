#!/usr/bin/env python3
import os
import glob
import re

def remove_regions(file_path):
    """Remove #region and #endregion directives from a file"""
    with open(file_path, 'r', encoding='utf-8-sig') as f:
        content = f.read()
    
    # Remove #region directives
    content = re.sub(r'#region\s+.*?\n', '', content)
    
    # Remove #endregion directives
    content = re.sub(r'#endregion\s*\n', '', content)
    
    # Clean up multiple consecutive blank lines
    content = re.sub(r'\n\s*\n\s*\n', '\n\n', content)
    
    # Write the modified content back to the file
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    
    print(f"Removed regions from {os.path.basename(file_path)}")
    return True

def main():
    # Get all .cs files in the IPNetworkTest directory
    directory_path = os.path.join('src', 'TestProject', 'IPNetworkTest')
    test_files = glob.glob(os.path.join(directory_path, '*.cs'))
    
    # Process each file
    successful = 0
    for file_path in test_files:
        if remove_regions(file_path):
            successful += 1
    
    print(f"Successfully updated {successful} of {len(test_files)} files.")

if __name__ == "__main__":
    main()