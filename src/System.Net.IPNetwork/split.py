import re
import os

def split_ipnetwork2_into_partials(input_file, output_dir=None):
    # Set output directory to current directory if not specified
    if output_dir is None:
        output_dir = os.path.dirname(input_file) or '.'
    
    # Create output directory if it doesn't exist
    if not os.path.exists(output_dir):
        os.makedirs(output_dir)
    
    # Read the input file
    with open(input_file, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Extract namespace declaration and class declaration
    namespace_match = re.search(r'namespace\s+([^\s{]+)', content)
    if not namespace_match:
        raise ValueError("Could not find namespace declaration")
    namespace = namespace_match.group(1)
    
    class_match = re.search(r'(public\s+(?:sealed\s+)?(?:partial\s+)?class\s+IPNetwork2[^{]*)', content)
    if not class_match:
        raise ValueError("Could not find IPNetwork2 class declaration")
    class_declaration = class_match.group(1)
    
    # Make sure class declaration includes 'partial' keyword
    if 'partial' not in class_declaration:
        class_declaration = class_declaration.replace('class', 'partial class')
        if 'partial' not in class_declaration:
            class_declaration = class_declaration.replace('public', 'public partial')
    
    # Find using directives
    using_directives = re.findall(r'using\s+[^;]+;', content)
    using_section = '\n'.join(using_directives)
    
    # Find all regions
    region_pattern = r'#region\s+([^\r\n]+)(.+?)#endregion'
    regions = re.findall(region_pattern, content, re.DOTALL)
    
    if not regions:
        print("No regions found in the file.")
        return
    
    # Create a file for each region
    for region_name, region_content in regions:
        # Clean region name for file name
        region_name = region_name.strip()
        sanitized_region_name = re.sub(r'[^\w]', '', region_name)
        file_name = f"IPNetwork2{sanitized_region_name}.cs"
        file_path = os.path.join(output_dir, file_name)
        
        # Create file content
        file_content = f"""// <copyright file="IPNetwork2{sanitized_region_name}.cs" company="IPNetwork">
// Copyright (c) IPNetwork. All rights reserved.
// </copyright>

namespace {namespace};

{class_declaration}
{{
    {region_content}
}}
"""
        
        # Write to file
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(file_content)
        
        print(f"Created {file_path}")
    
    print(f"Successfully split {len(regions)} regions into separate files.")

if __name__ == "__main__":
    import sys
    
    if len(sys.argv) > 1:
        input_file = sys.argv[1]
        output_dir = sys.argv[2] if len(sys.argv) > 2 else None
        split_ipnetwork2_into_partials(input_file, output_dir)
    else:
        print("Usage: python split_ipnetwork2.py <input_file> [output_directory]")
