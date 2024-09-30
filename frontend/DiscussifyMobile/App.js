import React from 'react';
import { StatusBar } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import { createDrawerNavigator } from '@react-navigation/drawer';
import BottomTabComponent from './src/components/BottomTabComponent'
import SearchBarComponent from './src/components/SearchBarComponent'
import { theme } from './src/components/theme';

const Drawer = createDrawerNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <StatusBar
        hidden={false}
        barStyle={theme.statusBarStyle}
        backgroundColor={theme.statusBarBackground}
      />
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

