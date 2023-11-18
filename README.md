# IS_Lab-2_ForwardChaining

# Rule-Based Inference System

## Overview

This repository contains a simple rule-based inference system implemented in C#. The program utilizes JSON files to store and manage facts and rules, allowing users to add facts, rules, generate new facts based on existing rules, and manipulate the data dynamically.

## Usage

1. **Add Facts:**
   - Users can add new facts to the system. If a fact already exists, the program informs the user.

2. **Add Rules:**
   - Users can add new rules to the system. The rules follow the format "if Antecedent, then Consequent."

3. **Generate New Facts:**
   - The system checks all rules and generates new facts based on satisfied antecedents.

4. **Delete Current Facts:**
   - Users can clear the existing facts, which will be saved as an empty list in the corresponding JSON file.

5. **Delete Current Rules:**
   - Users can clear the existing rules, which will be saved as an empty list in the corresponding JSON file.

6. **Exit:**
   - Exit the application.

## Dependencies

This program uses the Newtonsoft.Json library for JSON serialization and deserialization. Make sure to install the necessary dependencies before running the program.
