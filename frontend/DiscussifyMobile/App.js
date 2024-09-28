import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import BottomTabComponent from './src/components/BottomTabComponent'
import SearchBarComponent from './src/components/SearchBarComponent'

const Drawer = createDrawerNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Drawer.Navigator
        screenOptions={{
          header: () => <SearchBarComponent />
        }}
      >
        <Drawer.Screen name='Discussify' component={BottomTabComponent} />
      </Drawer.Navigator>
    </NavigationContainer>
  );
}

